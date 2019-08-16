using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace nanaki
{


    public class ScoreManager : MonoBehaviour
    {

        public Text scoreText;
        public Text lastScore;
        public float score;
        public bool reset;
        protected static ScoreManager instance;

        public static ScoreManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (ScoreManager) FindObjectOfType(typeof(ScoreManager));
                    if (instance == null)
                    {
                        Debug.LogError("PlayManager Instance Error");
                    }
                }

                return instance;
            }
        }

        private void Awake()
        {

            GameObject[] obj = GameObject.FindGameObjectsWithTag("ScoreManager");
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

        void Start()
        {

            score = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameStateManager.Instance.currentScene == 1 && reset == true)
            {

                scoreText.text = score.ToString("f0");
                lastScore.text = score.ToString("f0");
            }

        }

        public void ResetScore()
        {

            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            score = 0;
            reset = true;
        }

        public void SetScore(float num)
        {
            score += num;
        }
    }
}