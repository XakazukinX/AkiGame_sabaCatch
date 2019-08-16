using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayManager : MonoBehaviour {
    public GameObject player;
    public itemData iData;
    public Transform rBorder;
    public Transform lBorder;
    public Transform tBorder;
    public Transform bBorder;
    public static int adjust = 20;
    public GameObject item;
    public GameObject spawner;
    public nanaki.PlayerController PC;
    public GameObject result;
    bool tutrial;
    public GameObject tutObj;
    public GameObject voteMister;
    public GameObject voteMiss;
    public VoteUser voteuser;
    public Sprite thumbnail;
    public Text missName;
    public Text misterName;

    public enum State
    {
        Ready  = 0,
        Count  = 1,
        Play   = 2,
        End    = 3,
        Result = 4
    }


    public Text countText;
    private float countNum = 3f;
    private float currentTime;
    public int currentState;
    protected static PlayManager instance;
    public static PlayManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (PlayManager)FindObjectOfType(typeof(PlayManager));
                if (instance == null)
                {
                    Debug.LogError("PlayManager Instance Error");
                }
            }
            return instance;
        }
    }

    // Use this for initialization
    void Start () {
        currentState = 0;
        tutrial = true;
    }

    public void GameStart()
    {
        tutrial = false;
    }
    public void SpawnItem()
    {
        int x = (int)Random.Range(lBorder.position.x + adjust, rBorder.position.x - adjust);
        int y = (int)Random.Range(bBorder.position.y + adjust , tBorder.position.y - adjust);

        GameObject obj = (GameObject)Instantiate(item, new Vector2(x, y), Quaternion.identity);
        obj.transform.parent = spawner.transform;
        iData = obj.GetComponent<itemData>();
        
        obj.transform.parent = spawner.transform;

    }
    private void Awake()
    {
    
        GameObject[] obj = GameObject.FindGameObjectsWithTag("PlayManager");
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
    void Update()
    {
        if (currentState == (int)State.Ready)
        {
            if (tutrial)
                return;

                PC = GameObject.Find("Player").GetComponent<nanaki.PlayerController>();
                countText.text = "READY!!";
                currentState = (int)State.Count;
                nanaki.ScoreManager.Instance.ResetScore();
        }
        else if (currentState == (int)State.Count)
        {
            
            countNum -= Time.deltaTime;
            if (countNum <= 0)
            {
                countText.text = null;
                SpawnItem();
                currentState = (int)State.Play;
                countNum = 3;

            }
        }
        else if (currentState == (int)State.Play)
        {

            
            if (PC.dead)
            {
                currentState = (int)State.End;
            }
        }
        else if (currentState == (int)State.End)
        {
           result.SetActive(true);
            DisplayVote();
            ChandeResult();
        }
        else if (currentState == (int)State.Result)
        {

        }

    }

    private void DisplayVote()
    {
        string path;
        if (GameStateManager.Instance.selectMister != null)
        {
            voteMister.SetActive(true);
            voteuser = voteMister.GetComponent<VoteUser>();
            path = "Image/" + GameStateManager.Instance.selectMister;
            misterName.text = GameStateManager.Instance.selectMister;
            thumbnail = Resources.Load<Sprite>(path);
            voteuser.SetThumbnail(thumbnail);
        }
        if (GameStateManager.Instance.selectMiss != null)
        {
            voteMiss.SetActive(true);
            voteuser = voteMiss.GetComponent<VoteUser>();
            path = "Image/" + GameStateManager.Instance.selectMiss;
            missName.text = GameStateManager.Instance.selectMiss;
            thumbnail = Resources.Load<Sprite>(path);
            voteuser.SetThumbnail(thumbnail);
        }

    }
    public void ChangeReady()
    {
         foreach (Transform n in spawner.transform)
          {
             GameObject.Destroy(n.gameObject);
           }
        PC.dead = false;
        player.transform.localPosition = new Vector2(-19,0);
        result.SetActive(false);
        currentState = (int)State.Ready;
    }
    public void ChangeCount()
    {
        currentState = (int)State.Count;
    }
    public void ChandeEnd()
    {
        currentState = (int)State.End;
    }
    public void ChandeResult()
    {
        currentState = (int)State.Result;
    }
}
