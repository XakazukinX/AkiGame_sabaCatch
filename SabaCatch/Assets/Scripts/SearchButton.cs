using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SearchButton : MonoBehaviour {

    [SerializeField]private string word;
    Color selectColor;
    bool selected;
    // Use this for initialization
    void Start () {
		
	}
	
    public void SeachAct()
    {
        VoteManager.Instance.SearchPanel(word);
    }
}
