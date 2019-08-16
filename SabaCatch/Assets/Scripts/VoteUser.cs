using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class VoteUser : MonoBehaviour
{



    public string username;
    public string initial;
    public Text nameText;
    private Sprite thumbnail;
    Image charaImg;
    void Start()
    {

       
    }
    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetThumbnail(Sprite img)
    {
        charaImg = gameObject.GetComponent<Image>();
        thumbnail = img;
        charaImg.sprite = img;
    }

    public Sprite GetThumbnail()
    {
        return this.thumbnail;
    }
    public void SelectUser()
    {
        VoteManager.Instance.OpenDialog(this);
    }
    public void Apear()
    {
        gameObject.SetActive(true);

    }

    public void DisApear()
    {
        gameObject.SetActive(false);
    }

}	



