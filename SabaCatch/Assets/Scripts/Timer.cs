using System.Collections;
using System.Collections.Generic;
using falling;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private float gameTime;
    private float countTime;

    private int accelCount = 1;
    [SerializeField] private int accelSpanTime = 15;
    private bool stopTimer;

    [SerializeField] private GameObject gameEndUI;
    [SerializeField] private Text resultScore;
    [SerializeField] private Text resultSABA;

    private void Update()
    {
        if (stopTimer)
        {
            return;
        }
        
        gameTime -= Time.deltaTime;
        countTime += Time.deltaTime;
/*
        Debug.Log(countTime);
*/
        if (countTime > accelSpanTime * accelCount)
        {
            accelCount += 1;
            FallingManager.Instance.fallingSpeed -= 0.05f;
            FallingManager.Instance.fallingSpeed = Mathf.Clamp(FallingManager.Instance.fallingSpeed, 0.05f, 1);
            FallingManager.Instance.spawnWaitTime -= 0.25f;
            FallingManager.Instance.spawnWaitTime = Mathf.Clamp(FallingManager.Instance.spawnWaitTime, 0.2f, 5);
        }

        gameTime = Mathf.Clamp(gameTime, 0, 999);
        timerText.text = ((int)gameTime).ToString();

        if (gameTime <= 0)
        {
            gameEnd();
        }
        
        
    }

    private void gameEnd()
    {
        Debug.Log("GameEnd");
        FallingManager.Instance.stopSpawnObjects();
        stopTimer = true;
        
        gameEndUI.SetActive(true);
        resultScore.text = ScoreManager.Instance.scoreCount.ToString();
        resultSABA.text = ScoreManager.Instance.sabaCount.ToString();
    }
}