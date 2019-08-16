using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class VoteManager : MonoBehaviour {

    //オブジェクト
    public GameObject VotePop;
    public Text dialogName;
    public GameObject Dialog;
    public GameObject StartDialog;
    public GameObject BackButton;
    public GameObject PrevObj;
    public GameObject NextObj;
    public GameObject VoteMister;
    public GameObject VoteMiss;
    string settingName;
    public Text misterName;
    public Text missName;
    public Text pageText;
    public Text helpText;
    private List<GameObject> charaList = new List<GameObject>();
    public List<VoteUser> userData = new List<VoteUser>();
    public List<Sprite> thumbnail = new List<Sprite>();

    //検索用の頭文字(あ,か,さ,た,な,は,ま,や,ら,わ)
    public List<string[]> misterList = new List<string[]>();    //ミスター各情報を格納。CSVだよ
    public List<string[]> missList = new List<string[]>();      //ミス各情報を格納。CSVだよ
    public List<string> misterInitialList = new List<string>(); //検索用ミスターの頭文字を格納
    public List<string> missInitialList = new List<string>();   //検索用ミスの頭文字を格納
    public List<int> searchNumList = new List<int>();           //検索用の番地を格納
    public List<string[]> searchList = new List<string[]>();    //検索済みの情報を格納
    string searchWord;  //ボタンを押した時に検索用単語をドン

    bool missFlg;
    bool misterFlg;
    public VoteUser selectUser;
    public Sprite misterThumbnail;
    public Sprite missThumbnail;
    int differNum;  //ページに表示可能なキャラ数がセル数未満の時によしなにする為の
    int currentPhase;   //現在のフェイズ

    enum Phase
    {
        Mister = 0,
        Miss   = 1,
        Start  = 2
    }
    
    bool searchFlg;     //検索してるかいなか
    int maxPage;        //最大ページ数
    int maxNum;         //最大キャラ数(検索時は減少)
    public MisterLoader mister; 
    public MissLoader miss;
    private int dataNum = 8;
    private int cellNum = 8;    //セル数
    public int currentPage = 1; //現在のページ数

    protected static VoteManager instance;

    public static VoteManager Instance
    {

        get
        {
            if (instance == null)
            {
                instance = (VoteManager)FindObjectOfType(typeof(VoteManager));

                if (instance == null)
                {
                    Debug.LogError("VoteManager Instance Error");

                }
            }

            return instance;
        }

    }
    // Use this for initialization

    void Start() {

        currentPhase = (int)Phase.Mister;
        //キャラ表示用パネルの取得と格納
        for (int i = 1; i < cellNum; i++)
        {
            string objName = "Chara_" + i;
            var obj = GameObject.Find(objName);
            charaList.Add(obj);
        }


        currentPage = 1;

        //各ローダーからcsv読み込みさせて格納
        misterList = mister.LoadData(); 
        missList = miss.LoadData();

        for (int i = 0; i < misterList.Count; i++)
        {
            misterInitialList.Add(misterList[i][1]);
        }
        for (int i = 0; i < missList.Count; i++)
        {
            missInitialList.Add(missList[i][1]);
        }
        maxNum = misterList.Count;
        maxPage = maxNum / cellNum + 1;
        SetPanel(misterList, currentPage);
        PrevObj.SetActive(false);

    }

    private void SettingList() { }
    //通常時のパネルセット
    private void SetPanel(List<string[]> datas, int page)
    {

        dataNum = (page - 1) * cellNum;

        if (page == maxPage)
        {
            differNum = maxPage * cellNum - maxNum;
        }
        else {
            differNum = 0;
        }
        for (int i = 0; i < cellNum - differNum; i++)
        {
            userData[i].username = datas[i + dataNum][0];
            userData[i].SetName(userData[i].username);
            userData[i].initial = datas[i + dataNum][1];
            string path =  "Image/" + userData[i].username;
            Debug.Log(path);
            thumbnail[i] = Resources.Load<Sprite>(path);
            userData[i].SetThumbnail(thumbnail[i]);
            if (!userData[i].isActiveAndEnabled)
                userData[i].Apear();

        }
        //もし該当ページにデータが8個なかった場合は、ないデータ分のパネルを非表示にする
        if (page == maxPage)
        {
            for (int i = cellNum - differNum; i < cellNum; i++)
            {
                userData[i].DisApear();
            }
        }
    }


    //検索用の処理
    public void SearchPanel(string word)
    {
        if (Dialog.activeSelf)
            return;

        nanaki.SoundManager.Instance.PlaySE(0); //決定音
        if (currentPhase == (int)Phase.Start)
            return;

        searchWord = word;
        searchNumList.Clear();
        searchList.Clear();
        if (currentPhase == (int)Phase.Mister) {
            for (int i = 0; i < misterInitialList.Count; i++)
            {
                if (misterInitialList[i] == searchWord)
                {
                    searchNumList.Add(i);
                }
            }
            for (int i = 0; i < searchNumList.Count; i++)
            {
                searchList.Add(misterList[searchNumList[i]]);
            }
        }
        else if (currentPhase == (int)Phase.Miss)
        {
                for (int i = 0; i < missInitialList.Count; i++)
                {
                    if (missInitialList[i] == searchWord)
                    {
                        searchNumList.Add(i);
                    }
                }
            for (int i = 0; i < searchNumList.Count; i++)
            {
                searchList.Add(missList[searchNumList[i]]);
            }
        }
         
        maxNum = searchList.Count;
        maxPage = maxNum / cellNum + 1;
        if (maxPage == 1)
        {
            NextObj.SetActive(false);
            PrevObj.SetActive(false);
        }
        else
        {
            NextObj.SetActive(true);
            PrevObj.SetActive(false);
        }
        currentPage = 1;
        pageText.text = currentPage.ToString() + "ページ目";
        searchFlg = true;
        SetPanel(searchList, currentPage);

    }

    public void OpenDialog(VoteUser data){

        if (currentPhase != (int)Phase.Start)
        {
            selectUser = data;
            dialogName.text = selectUser.username;
            Dialog.SetActive(true);
        }

    }

    public void Yes()
    {
        if (currentPhase == (int)Phase.Mister)
        {
            helpText.text = "投票したいミスを選択してください";
            VoteMister.SetActive(true);
            misterName.text = selectUser.username;
            VoteMister.GetComponent<Image>().sprite = selectUser.GetThumbnail();
            Dialog.SetActive(false);
            currentPhase = (int)Phase.Miss;
            BackButton.SetActive(true);
            currentPage = 2;
            searchFlg = false;
            maxNum = missList.Count;
            maxPage = maxNum / cellNum + 1;
            PrevPage();
            GameStateManager.Instance.selectMister = misterName.text;
            misterFlg = false;
        }
        else if (currentPhase == (int)Phase.Miss)
        {
            VoteMiss.SetActive(true);
            missName.text = selectUser.username;
            VoteMiss.GetComponent<Image>().sprite = selectUser.GetThumbnail();
            //VotePop.SetActive(false);
            Dialog.SetActive(false);
            GameStateManager.Instance.selectMiss = missName.text;
            currentPhase = (int)Phase.Start;
            StartDialog.SetActive(true);
            missFlg = false;
        } else if (currentPhase == (int)Phase.Start) {

            if (misterFlg)
                GameStateManager.Instance.selectMister = null;
            if (missFlg)
                GameStateManager.Instance.selectMiss = null;
            
            VotePop.SetActive(false);
        }


    }

    public void No()
    {
        nanaki.SoundManager.Instance.PlaySE(0); //決定音
        Dialog.SetActive(false);
        if(currentPhase == (int)Phase.Start)
        {
            StartDialog.SetActive(false);
            currentPhase = (int)Phase.Miss;
        }
    }

    
    //
    public void BackSelect()
    {
        if (Dialog.activeSelf)
            return;
        if (currentPhase == (int)Phase.Miss)
        {
            helpText.text = "投票したいミスターを選択してください";
            currentPhase = (int)Phase.Mister;
            maxNum = misterList.Count;
            maxPage = maxNum / cellNum + 1;
            currentPage = 2;
            searchFlg = false;
            PrevPage();
            BackButton.SetActive(false);
        }
    }

    //ミス、またはミスターいずれのみしか選択しない人向け
    public void Skip()
    {
        if (Dialog.activeSelf)
            return;
        if (currentPhase == (int)Phase.Mister)
        {
            helpText.text = "投票したいミスを選択してください";
            currentPhase = (int)Phase.Miss;
            BackButton.SetActive(true);
            currentPage = 2;
            searchFlg = false;
            maxNum = missList.Count;
            maxPage = maxNum / cellNum + 1;
            PrevPage();
            VoteMister.SetActive(false);
            misterFlg = true;
        }else if (currentPhase == (int)Phase.Miss)
        {
            currentPhase = (int)Phase.Start;
            VoteMiss.SetActive(false);
            StartDialog.SetActive(true);
            StartDialog.SetActive(true);
            missFlg = true; 
        }
    }

    //ページを進める
    public void NextPage()
    {
        if (Dialog.activeSelf)
            return;
        nanaki.SoundManager.Instance.PlaySE(0); //決定音
        if (currentPhase == (int)Phase.Mister)
        {
            if (!searchFlg)
            {
                currentPage++;
                SetPanel(misterList, currentPage);

                if (currentPage == maxPage)
                    NextObj.SetActive(false);

                if (!PrevObj.activeSelf)
                    PrevObj.SetActive(true);
                pageText.text = currentPage.ToString() + "ページ目";

            }
            else
            {
                currentPage++;
                SetPanel(searchList, currentPage);

                if (currentPage == maxPage)
                    NextObj.SetActive(false);

                if (!PrevObj.activeSelf)
                    PrevObj.SetActive(true);
                pageText.text = currentPage.ToString() + "ページ目";
            }
        }
        else if(currentPhase == (int)Phase.Miss)
        {
            if (!searchFlg)
            {
                currentPage++;
                SetPanel(missList, currentPage);

                if (currentPage == maxPage)
                    NextObj.SetActive(false);

                if (!PrevObj.activeSelf)
                    PrevObj.SetActive(true);
                pageText.text = currentPage.ToString() + "ページ目";

            }
            else
            {
                currentPage++;
                SetPanel(searchList, currentPage);

                if (currentPage == maxPage)
                    NextObj.SetActive(false);

                if (!PrevObj.activeSelf)
                    PrevObj.SetActive(true);
                pageText.text = currentPage.ToString() + "ページ目";
            }
        }
    }

    //前のパネルに戻る
    public void PrevPage()
    {
        if (Dialog.activeSelf)
            return;
        nanaki.SoundManager.Instance.PlaySE(0); //決定音
        if (currentPhase == (int)Phase.Mister)
        {
            if (!searchFlg)
            {
                currentPage--;
                SetPanel(misterList, currentPage);

                if (currentPage < maxPage)
                    NextObj.SetActive(true);
                if(currentPage == 1)
                    PrevObj.SetActive(false);

                pageText.text = currentPage.ToString() + "ページ目";
            }
            else
            {
                currentPage--;
                SetPanel(searchList, currentPage);

                if (currentPage < maxPage)
                    NextObj.SetActive(true);
                if (currentPage == 1)
                    PrevObj.SetActive(false);

                pageText.text = currentPage.ToString() + "ページ目";
            }
        }
        else if (currentPhase == (int)Phase.Miss)
        {
            if (!searchFlg)
            {
                currentPage--;
                SetPanel(missList, currentPage);

                if (currentPage < maxPage)
                    NextObj.SetActive(true);
                if (currentPage == 1)
                    PrevObj.SetActive(false);

                pageText.text = currentPage.ToString() + "ページ目";
            }
            else
            {
                currentPage--;
                SetPanel(searchList, currentPage);

                if (currentPage < maxPage)
                    NextObj.SetActive(true);
                if (currentPage == 1)
                    PrevObj.SetActive(false);

                pageText.text = currentPage.ToString() + "ページ目";
            }

        }
    }

}
