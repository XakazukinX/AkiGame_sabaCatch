using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private AudioSource[] audiosorces;

    private void Start()
    {
        audiosorces = GetComponents<AudioSource>();
    }

    public void playSound(int soundNum)
    {
        audiosorces[soundNum].Play();
    }
}
