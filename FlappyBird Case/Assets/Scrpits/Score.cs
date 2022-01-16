using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [HideInInspector] public int currentScore;
    


    private void Start()
    {
        BirdController birdController = FindObjectOfType<BirdController>();
        birdController.onScorePoint += onUpdateScore;


        scoreText = GetComponent<TextMeshProUGUI>();
        currentScore = 0;
        onUpdateScore(0);
    }

    public void onUpdateScore(int value)
    {
        currentScore += value;
        scoreText.text = currentScore.ToString();
    }
}
