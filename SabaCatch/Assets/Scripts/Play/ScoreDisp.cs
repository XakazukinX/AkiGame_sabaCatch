using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreDisp : MonoBehaviour {

    public float score;
    public Text scoreText;
	// Use this for initialization
	void Start () {
        score = nanaki.ScoreManager.Instance.score;
        scoreText.text = score.ToString();
        nanaki.ScoreManager.Instance.reset = false;

    }
	

}
