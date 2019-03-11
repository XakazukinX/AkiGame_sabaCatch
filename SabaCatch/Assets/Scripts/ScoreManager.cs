using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    [SerializeField] private Text gameScoreText;

    [SerializeField] private int scoreWeight;
    private int gameScore;

    public void additionScore()
    {
        gameScore += scoreWeight;
        gameScoreText.text = setScoreText();
    }
    
    public void subtractionScore()
    {
        gameScore -= scoreWeight;
        gameScoreText.text = setScoreText();
    }

    private string setScoreText()
    {
        string handred = (gameScore / 100).ToString();
        string ten = (gameScore % 100 / 10).ToString();
        string one = (gameScore % 100 % 10 / 1).ToString();
        return handred + ten + one;
    }
    
}
