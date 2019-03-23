using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject[] GameManagers;
    [SerializeField] private GameObject playerObject;

    private void OnEnable()
    {
        for (int i = 0; i < GameManagers.Length; i++)
        {
            GameManagers[i].SetActive(true);
        }
        playerObject.SetActive(true);
        SoundManager.Instance.playSound(2);
    }
}
