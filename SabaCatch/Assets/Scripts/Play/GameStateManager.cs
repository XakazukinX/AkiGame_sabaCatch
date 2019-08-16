using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStateManager : MonoBehaviour {

    
    public enum Scene
    {
        Title  = 0,
        Play   = 1,
        Result = 2
    }
    public string voteName;         //投票名
    public int currentScene = 0;    //現在のシーン
    public string selectMister;
    public string selectMiss;
    bool tutorial;  //初回限定で説明文を表示
    bool soundFlg;
    public GameObject tutObj;
    protected static GameStateManager instance;
    public static GameStateManager Instance
    {

        get
        {
            if (instance == null)
            {
                instance = (GameStateManager)FindObjectOfType(typeof(GameStateManager));

                if (instance == null)
                {
                    Debug.LogError("GameStateManager Instance Error");

                }
            }

            return instance;
        }

    }
    private void Awake()
    {
        currentScene = (int)Scene.Title;
        GameObject[] obj = GameObject.FindGameObjectsWithTag("GameStateManager");
        if (obj.Length > 1)
        {
            // 既に存在しているなら削除
            Destroy(gameObject);
        }
        else
        {
            // 音管理はシーン遷移では破棄させない
            DontDestroyOnLoad(gameObject);
        }
    }
    
   

    // Update is called once per frame
    void Update() {

        if (soundFlg)
            return;
        if (currentScene == (int)Scene.Play)
        {
            
            nanaki.SoundManager.Instance.PlayBGM(0);
            soundFlg = true;


        }
        else if (currentScene == (int)Scene.Result)
        {

        }

     }

    public void ChangePlay()
    {
        SceneManager.LoadScene("MainScene");
        currentScene = (int)Scene.Play;
    }

}
