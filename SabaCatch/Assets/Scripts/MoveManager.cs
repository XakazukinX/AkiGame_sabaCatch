using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour {
    
    [SerializeField] private KeyCode[] keys;
    public static Action<int> OnGetMove;

    private void Update()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                if (OnGetMove != null)
                {
                    OnGetMove.Invoke(i);
                }
            }
        }
    }
    
}
