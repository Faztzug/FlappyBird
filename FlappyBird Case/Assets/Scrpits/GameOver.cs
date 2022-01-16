using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameOver : MonoBehaviour
{
    private Animator anim;
    private int scoreValue;
    private int bestScore;
    private TextMeshProUGUI score;
    private TextMeshProUGUI best;
    [SerializeField] float waitToScoreCount = 1f;
    [SerializeField] float timeScoreCount = 1f;
    public float currentScoreCountValue = 0;
    public float scoreParts;

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

        //scoreValue = FindObjectOfType<Score>().currentScore;
        //score.text = scoreValue.ToString();

        best.text = bestScore.ToString();

        StartCoroutine(ScoreCount(waitToScoreCount));


        
    }

    IEnumerator ScoreCount(float wait)
    {
        yield return new WaitForSeconds(wait);

        scoreValue = FindObjectOfType<Score>().currentScore;
        

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
