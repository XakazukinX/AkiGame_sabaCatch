using System.Collections;
using System.Collections.Generic;
using TweetWithScreenShot;
using UnityEngine;

public class ResultManager : MonoBehaviour {
    public float score;
    private string mister;
    private string miss;
    private string vote   = "に投票　　";
    private string title  = "Sで";
    private string footter = "点を獲得！！";
    public CustomTweetManager ctweet;
    // Use this for initialization
    void Start ()
    {
        score = ScoreManager.Instance.scoreCount;
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void Retry()
    {
        PlayManager.Instance.ChangeReady();

    } 
   
   
    public void Tweet()
    {
        if(GameStateManager.Instance.selectMister != null)
        mister = GameStateManager.Instance.selectMister + vote;

        if(GameStateManager.Instance.selectMiss != null)
        miss = GameStateManager.Instance.selectMiss + vote;
        score = nanaki.ScoreManager.Instance.score;
        StartCoroutine(CustomTweetManager.Instance.TweetWithScreenShot(mister + miss + title + score + footter));
    }

}
