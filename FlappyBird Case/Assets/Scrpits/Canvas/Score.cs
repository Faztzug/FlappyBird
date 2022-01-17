using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class Score : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI scoreText;
    //[HideInInspector] 
    public int currentScore;
    private char[] textScore;

    private Image[] digits;

    [SerializeField] GameObject numberPrefab;

    [SerializeField] private Sprite n0;
    [SerializeField] private Sprite n1;
    [SerializeField] private Sprite n2;
    [SerializeField] private Sprite n3;
    [SerializeField] private Sprite n4;
    [SerializeField] private Sprite n5;
    [SerializeField] private Sprite n6;
    [SerializeField] private Sprite n7;
    [SerializeField] private Sprite n8;
    [SerializeField] private Sprite n9;



    private void Start()
    {
        BirdController birdController = FindObjectOfType<BirdController>();
        birdController.onScorePoint += onUpdateScore;

        digits = GetComponentsInChildren<Image>();


        //scoreText = GetComponentInChildren<TextMeshProUGUI>();
        currentScore = 0;
        onUpdateScore(0);
    }

    public void onUpdateScore(int value)
    {
        currentScore += value;
        textScore = currentScore.ToString().ToCharArray();


        int textScoreLenght = textScore.Length;

        if (textScoreLenght > digits.Length)
        {
            Instantiate(numberPrefab, this.gameObject.transform);

            digits = GetComponentsInChildren<Image>();
        }



        for (int i = 0; i < textScoreLenght; i++)
        {
            switch (textScore[i])
            {
                case '0':
                    digits[i].sprite = n0;
                    break;
                case '1':
                    digits[i].sprite = n1;
                    break;
                case '2':
                    digits[i].sprite = n2;
                    break;
                case '3':
                    digits[i].sprite = n3;
                    break;
                case '4':
                    digits[i].sprite = n4;
                    break;
                case '5':
                    digits[i].sprite = n5;
                    break;
                case '6':
                    digits[i].sprite = n6;
                    break;
                case '7':
                    digits[i].sprite = n7;
                    break;
                case '8':
                    digits[i].sprite = n8;
                    break;
                case '9':
                    digits[i].sprite = n9;
                    break;
            }
        }


        foreach (char digit in textScore)
        {
            
                
        }
        //scoreText.text = currentScore.ToString();
    }

    public void onGameOver()
    {
        //scoreText.text = "";

        foreach (Image digit in digits)
        {
            digit.gameObject.SetActive(false);
        }
    }
}
