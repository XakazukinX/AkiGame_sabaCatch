using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tail : MonoBehaviour
{
    private enum Dir
    {
        Up,
        Down,
        Left,
        Right
    }
    float time;
    float alpha;
    Color color;
    public Image fade;
    public GameObject canvas;
    private bool flg = true;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(SetStatus());

    }

    // Update is called once per frame
    void Update()
    {
        if (!flg)
            StartCoroutine(Change());


    }
    public IEnumerator SetStatus()
    {
        while (alpha < 1f)
        {
            fade.color = new Color(255, 255, 255, alpha);
            alpha += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        alpha = 1;

        this.GetComponent<BoxCollider2D>().enabled = true;
        flg = false;
    }


    public IEnumerator Change()
    {
        time = Random.Range(0.01f,0.05f);
        flg = true;
        while (alpha >= 0f)
        {
            fade.color = new Color(255, 255, 255, alpha);
            alpha -= 0.1f;
            yield return new WaitForSeconds(time);
        }
        while (alpha <= 1f)
        {
            fade.color = new Color(255, 255, 255, alpha);
            alpha += 0.1f;
            yield return new WaitForSeconds(time);
        }
        flg = false;
    }
}