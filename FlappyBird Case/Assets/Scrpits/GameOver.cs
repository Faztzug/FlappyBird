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
    private TextMeshProUGUI score;
    private TextMeshProUGUI best;
    [SerializeField] float waitToScoreCount = 1f;
    [SerializeField] float timeScoreCount = 1f;
    private float currentScoreCountValue = 0;
    private float scoreParts;

    [SerializeField] private Image medalImage;
    [SerializeField] private Sprite tinMedal;
    [SerializeField] private Sprite bronzeMedal;
    [SerializeField] private Sprite silverMedal;
    [SerializeField] private Sprite goldMedal;

    private void Start()
    {
        anim = GetComponent<Animator>();

        TextMeshProUGUI[] texts = transform.GetComponentsInChildren<TextMeshProUGUI>();

        score = texts[0];
        best = texts[1];

        LoadGame();

    }


    public void EndGame()
    {
        anim.SetBool("GameOver", true);

        scoreValue = FindObjectOfType<Score>().currentScore;
        //scoreValue = FindObjectOfType<Score>().currentScore;
        //score.text = scoreValue.ToString();

        best.text = bestScore.ToString();

        Medal();

        StartCoroutine(ScoreCount(waitToScoreCount));


        
    }

    private void Medal()
    {
        if(scoreValue < 4)
        {

        }else if (scoreValue < 10)
        {
            medalImage.color = Color.white;
            medalImage.sprite = tinMedal;
        }
        else if (scoreValue < 20)
        {
            medalImage.color = Color.white;
            medalImage.sprite = bronzeMedal;
        }
        else if (scoreValue < 35)
        {
            medalImage.color = Color.white;
            medalImage.sprite = silverMedal;
        }
        else if (scoreValue >= 35)
        {
            medalImage.color = Color.white;
            medalImage.sprite = goldMedal;
        }
    }

    IEnumerator ScoreCount(float wait)
    {
        yield return new WaitForSeconds(wait);

        
        

        score.text = ((int)currentScoreCountValue).ToString();

        scoreParts =  scoreValue * timeScoreCount/60;

        StartCoroutine(ScoreCounting(timeScoreCount/60));


    }

    IEnumerator ScoreCounting(float wait)
    {
        yield return new WaitForSeconds(wait);

        if (currentScoreCountValue < scoreValue)
        {
            currentScoreCountValue += scoreParts;
            score.text = ((int)currentScoreCountValue).ToString();

            Debug.Log("current Score Value: " + currentScoreCountValue);

            StartCoroutine(ScoreCounting(wait));
        }
        else
        {
            score.text = scoreValue.ToString();

            if (scoreValue > bestScore)
            {
                bestScore = scoreValue;
                best.text = bestScore.ToString();

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
