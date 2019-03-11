using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    [SerializeField] private Text gameScoreText;

    [SerializeField] private int scoreWeight;
    private int gameScore;

    [HideInInspector] public int scoreCount;
    [HideInInspector] public int sabaCount;

    public void additionScore()
    {
        gameScore += scoreWeight;
        gameScoreText.text = setScoreText();
        scoreCount = gameScore;
    }
    
    public void subtractionScore()
    {
        gameScore -= (scoreWeight*5);
        gameScoreText.text = setScoreText();
        scoreCount = gameScore;
        sabaCount += 1;
    }

    private string setScoreText()
    {
        string handred = (gameScore / 100).ToString();
        string ten = (gameScore % 100 / 10).ToString();
        string one = (gameScore % 100 % 10 / 1).ToString();
        return handred + ten + one;
    }
    
}
