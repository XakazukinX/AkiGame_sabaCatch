using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditButton : MonoBehaviour
{

    [SerializeField] private GameObject CreditUI;
    [SerializeField] private GameObject crButton;


    public void OnClick()
    {
        CreditUI.SetActive(false);
        crButton.SetActive(false);
    }
}
