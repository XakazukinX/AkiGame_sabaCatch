using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweetButton : MonoBehaviour
{

    private string tweetText;
    [SerializeField] private string hashtag = "夏秋ゲーム";
    
    
    public void OnClick()
    {
        tweetText = "SABA_GAMEで" + ScoreManager.Instance.scoreCount + "点を獲得しました！\n 獲得した鯖は【"+ ScoreManager.Instance.sabaCount+"】匹！ ";
        var url = "https://twitter.com/intent/tweet?"
                  + "text=" + tweetText
                  + "&hashtags=" + hashtag;

#if UNITY_EDITOR
        Application.OpenURL ( url );
#elif UNITY_WEBGL
            // WebGLの場合は、ゲームプレイ画面と同じウィンドウでツイート画面が開かないよう、処理を変える
            Application.ExternalEval(string.Format("window.open('{0}','_blank')", url));
#else
            Application.OpenURL(url);
#endif
    }
}
