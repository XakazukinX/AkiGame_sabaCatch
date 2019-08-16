using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeSwitch : MonoBehaviour {

    private Image currentImg;
    public Sprite onImg;
    public Sprite offImg;
    public Text buttonText;
    bool volumeFlg = true;

    private void Start()
    {
        currentImg = GetComponent<Image>();
    }
    public void VolumeChange()
    {
        if (volumeFlg)
        {
            currentImg.sprite = offImg;
            buttonText.text = "VOLUME：OFF";
            nanaki.SoundManager.Instance.volume.Mute = true;
            volumeFlg = false;

        } else if(!volumeFlg){
            currentImg.sprite = onImg;
            buttonText.text = "VOLUME：ON";
            nanaki.SoundManager.Instance.volume.Mute = false;
            volumeFlg = true;
        }
        
    }
}
