using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class itemData : MonoBehaviour
{
   // public int size[3] = new int [20,30,40];

    public int num;
    public int id;
    public static float limitTime;
    public float currentTime;
    public Transform rBorder;
    public Transform lBorder;
    public Transform tBorder;
    public Transform bBorder;
    public static int adjust = 20;
    // Use this for initialization
    void Start()
    {
        num = Random.Range(1,9);
        var txt = transform.GetChild(0).gameObject.GetComponent<Text>();
        txt.text = num.ToString();
        rBorder = GameObject.Find("RightBorder").transform;
        lBorder = GameObject.Find("LeftBorder").transform;
        tBorder = GameObject.Find("UpBorder").transform;
        bBorder = GameObject.Find("BottomBorder").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int x = (int)Random.Range(lBorder.position.x + adjust, rBorder.position.x - adjust);
        int y = (int)Random.Range(bBorder.position.y + adjust, tBorder.position.y - adjust);
        transform.position = new Vector2(x,y);


    }
}
