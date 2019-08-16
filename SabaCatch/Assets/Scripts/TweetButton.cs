using System.Collections;
using System.Collections.Generic;
using TweetWithScreenShot;
using UnityEngine;



public class TweetButton : MonoBehaviour
{

    private string tweetText;
    private string mister;
    private string miss;
    private string vote   = "に投票　　";
    private string title  = "炎上回避！投票Getterで";
    private string footter = "点を獲得！！";
    public CustomTweetManager ctweet;
    
    public void Retry()
    {
        PlayManager.Instance.ChangeReady();
    } 
   
   
    public void OnClick()
    {
        if(GameStateManager.Instance.selectMister != null)
            mister = GameStateManager.Instance.selectMister + vote;

        if(GameStateManager.Instance.selectMiss != null)
            miss = GameStateManager.Instance.selectMiss + vote;
        StartCoroutine(CustomTweetManager.Instance.TweetWithScreenShot(mister + miss + title + ScoreManager.Instance.scoreCount + footter));
    }
    

}
