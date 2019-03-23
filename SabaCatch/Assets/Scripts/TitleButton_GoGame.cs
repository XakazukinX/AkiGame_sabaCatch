using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton_GoGame : MonoBehaviour {

    public void Onclick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
