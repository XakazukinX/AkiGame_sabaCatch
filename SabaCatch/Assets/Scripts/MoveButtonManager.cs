using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButtonManager : MonoBehaviour
{

    public int buttonNumber;
    
    public void OnClicked()
    {
        if (MoveManager.OnGetMove != null)
        {
            MoveManager.OnGetMove.Invoke(buttonNumber);
        }
    }
}
