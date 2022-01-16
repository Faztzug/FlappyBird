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
    [SerializeField] float timeScoreCount = 1f;

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
        score.text = scoreValue.ToString();
        if (scoreValue > bestScore)
            bestScore = scoreValue;
        best.text = bestScore.ToString();

        SaveGame();
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
