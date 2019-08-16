using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace nanaki
{



    public class PlayerController : MonoBehaviour
    {

        itemData iData;
        public bool dead; //生存フラグ
        public Vector2 lastPos;
        public List<Transform> tail = new List<Transform>(); //しっぽ
        public GameObject[] tailObj = new GameObject[4];
        private bool eat;
        public GameObject spawner;
        private int tmp;

        private enum Dir
        {
            Up,
            Down,
            Left,
            Right
        }

        public enum State
        {
            Ready = 0,
            Count = 1,
            Play = 2,
            End = 3
        }

        float score;
        private string currentDir;

        public float speed;

        // Use this for initialization
        void Start()
        {
            speed = 2;
            currentDir = Dir.Right.ToString();


        }

        // Update is called once per frame
        void Update()
        {
            if (!dead && PlayManager.Instance.currentState == (int) State.Play)
            {

                movePlayer();
            }


        }

        public void LeftChange()
        {
            currentDir = Dir.Left.ToString();
        }

        public void UpChange()
        {
            currentDir = Dir.Up.ToString();

        }

        public void DownChange()
        {
            currentDir = Dir.Down.ToString();

        }

        public void RightChange()
        {
            currentDir = Dir.Right.ToString();
        }

        //キャラを動かす
        private void movePlayer()
        {
            //上向きの
            if (currentDir == Dir.Up.ToString())
            {
                this.transform.position += new Vector3(0, speed, 0);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.transform.position += new Vector3(0, speed, 0);
                currentDir = Dir.Up.ToString();
            }

            if (currentDir == Dir.Down.ToString())
            {
                this.transform.position += new Vector3(0, -speed, 0);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.transform.position += new Vector3(0, -speed, 0);
                currentDir = Dir.Down.ToString();
            }

            if (currentDir == Dir.Left.ToString())
            {
                this.transform.position += new Vector3(-speed, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.transform.position += new Vector3(-speed, 0, 0);
                currentDir = Dir.Left.ToString();
            }



            if (currentDir == Dir.Right.ToString())
            {
                this.transform.position += new Vector3(speed, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.transform.position += new Vector3(speed, 0, 0);
                currentDir = Dir.Right.ToString();
            }

        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Item")
            {
                SoundManager.Instance.PlaySE(1); //アイテム取得音
                iData = collision.GetComponent<itemData>();
                score = iData.num;
                ScoreManager.Instance.SetScore(score);
                speedUp();
                int rnd = Random.Range(0, 4);
                GameObject g = (GameObject) Instantiate(tailObj[rnd], transform.position, Quaternion.identity);
                g.transform.parent = spawner.transform;
                tail.Insert(0, g.transform);
                Destroy(collision.gameObject);
                PlayManager.Instance.SpawnItem();
            }
            else
            {
                SoundManager.Instance.PlaySE(2); //爆破音
                speed = 2;
                PlayManager.Instance.ChandeEnd();
                dead = true;
            }

        }

        public void speedUp()
        {
            score = ScoreManager.Instance.score;
            if (score > 25 && speed == 2)
            {
                speed++;
            }
            else if (score > 50 && speed == 3)
            {
                speed++;
            }
            else if (score > 75 && speed == 4)
            {
                speed++;
            }
            else if (score > 100 && speed == 5)
            {
                speed++;
            }
            else
            {

            }
        }


    }
}
