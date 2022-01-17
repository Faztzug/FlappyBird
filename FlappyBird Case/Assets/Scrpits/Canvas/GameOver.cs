using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Animator anim;
    private int scoreValue;
    private int bestScore;
    private ScoreToSprite score;
    private ScoreToSprite best;
    //private TextMeshProUGUI score;
    //private TextMeshProUGUI best;
    [SerializeField] float waitToScoreCount = 1f;
    [SerializeField] float timeScoreCount = 1f;
    private float currentScoreCountValue = 0;
    private float scoreParts;

    [SerializeField] private Image medalImage;
    [SerializeField] private Sprite tinMedal;
    [SerializeField] private int tinRemoveSparkles;
    [SerializeField] private Sprite bronzeMedal;
    [SerializeField] private int bronzeRemoveSparkles;
    [SerializeField] private Sprite silverMedal;
    [SerializeField] private int silverRemoveSparkles;
    [SerializeField] private Sprite goldMedal;

    [SerializeField] private Image newImage;

    private bool endCalled = false;

    private void Start()
    {
        anim = GetComponent<Animator>();

        ScoreToSprite[] texts = transform.GetComponentsInChildren<ScoreToSprite>();

        score = texts[0];
        best = texts[1];

        LoadGame();

    }


    public void EndGame()
    {
        if(endCalled == false)
        {
            endCalled = true;

            anim.SetBool("GameOver", true);

            scoreValue = FindObjectOfType<Score>().currentScore;
            //scoreValue = FindObjectOfType<Score>().currentScore;
            //score.text = scoreValue.ToString();

            //best.text = bestScore.ToString();
            best.UpdateScore(bestScore);

            Medal();

            StartCoroutine(ScoreCount(waitToScoreCount));

        }
    }

    private void Medal()
    {
        if(scoreValue < 4)
        {
            foreach (Sparkle item in GetComponentsInChildren<Sparkle>())
            {
                item.gameObject.SetActive(false);
            }
        }else if (scoreValue >= 4 && scoreValue < 10)
        {
            MedalSparkleRemove(tinRemoveSparkles);

            medalImage.color = Color.white;
            medalImage.sprite = tinMedal;
        }
        else if (scoreValue >= 10 && scoreValue < 20)
        {
            MedalSparkleRemove(bronzeRemoveSparkles);

            medalImage.color = Color.white;
            medalImage.sprite = bronzeMedal;
        }
        else if (scoreValue >= 20 && scoreValue < 35)
        {
            MedalSparkleRemove(silverRemoveSparkles);
            
            medalImage.color = Color.white;
            medalImage.sprite = silverMedal;
        }
        else if (scoreValue >= 35)
        {
            medalImage.color = Color.white;
            medalImage.sprite = goldMedal;
        }
    }

    private void MedalSparkleRemove(int removeN)
    {
        Sparkle[] sparkles = GetComponentsInChildren<Sparkle>();

        Debug.Log("Sparkles: " + sparkles.Length);

        for (int i = 0; i < removeN; i++)
        {
            sparkles[i].gameObject.SetActive(false);

            //Debug.Log("Removed " + (i + 1) + " Sparkles.");
        }
    }

    IEnumerator ScoreCount(float wait)
    {
        yield return new WaitForSeconds(wait);




        //score.text = ((int)currentScoreCountValue).ToString();
        score.UpdateScore((int)currentScoreCountValue);

        scoreParts =  scoreValue * timeScoreCount/60;

        StartCoroutine(ScoreCounting(timeScoreCount/60));


    }

    IEnumerator ScoreCounting(float wait)
    {
        yield return new WaitForSeconds(wait);

        if (currentScoreCountValue < scoreValue)
        {
            currentScoreCountValue += scoreParts;
            //score.text = ((int)currentScoreCountValue).ToString();
            score.UpdateScore((int)currentScoreCountValue);

            //Debug.Log("current Score Value: " + currentScoreCountValue);

            StartCoroutine(ScoreCounting(wait));
        }
        else
        {
            //score.text = scoreValue.ToString();
            score.UpdateScore(scoreValue);

            if (scoreValue > bestScore)
            {
                newImage.color = Color.white;
                bestScore = scoreValue;
                //best.text = bestScore.ToString();
                best.UpdateScore(bestScore);

                SaveGame();
            }
                
        }
    }


    public void SaveGame()
    {
        

        FileStream flappyFile = File.Create(Application.persistentDataPath + "flappy.sav");
        BinaryFormatter bfFlappy = new BinaryFormatter();
        bfFlappy.Serialize(flappyFile, bestScore);
        flappyFile.Close();

        
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "flappy.sav"))
        {


            FileStream file = File.Open(Application.persistentDataPath + "flappy.sav", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            int loadedSave = (int)bf.Deserialize(file);

            bestScore = loadedSave;

            file.Close();
        }

        
    }
}
