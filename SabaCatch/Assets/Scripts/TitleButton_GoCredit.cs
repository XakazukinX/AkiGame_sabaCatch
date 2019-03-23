using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton_GoCredit : MonoBehaviour {

	[SerializeField] private GameObject CreditUI;
	[SerializeField] private GameObject crButton;
	public void OnClick()
	{
		CreditUI.SetActive(true);
		crButton.SetActive(true);
	}
}
