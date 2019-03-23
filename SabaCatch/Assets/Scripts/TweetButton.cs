using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweetButton : MonoBehaviour
{

    private string tweetText;
    [SerializeField] private string hashtag = "夏秋ゲーム";
    
    
    public void OnClick()
    {
        tweetText = "おちさば!!で" + ScoreManager.Instance.scoreCount + "点を獲得しました！\n 獲得した鯖は【"+ ScoreManager.Instance.sabaCount+"】匹！ ";
        var url = "https://twitter.com/intent/tweet?"
                  + "text=" + tweetText
                  + "&hashtags=" + hashtag;
        
        
#if UNITY_EDITOR
        Application.OpenURL ( url );
#elif UNITY_WEBGL
        naichilab.UnityRoomTweet.Tweet ("natsuakigame_ochisaba", tweetText, "夏秋ゲーム");
#else
            Application.OpenURL(url);
#endif
    }
}
