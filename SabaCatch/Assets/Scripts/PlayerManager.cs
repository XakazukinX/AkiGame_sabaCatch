using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    //Player本体
    [SerializeField] private GameObject player;
    [HideInInspector] public GameObject _player;
    
    //Playerの移動回数を管理
    [HideInInspector] public int playerMoveCount = 0;
    [SerializeField] private int maxPlayerMoveCount;
    [HideInInspector] public int _maxPlayerMoveCount;
    [SerializeField] private int minPlayerMoveCount;
    [HideInInspector] public int _minPlayerMoveCount;
    
    //実際の移動距離
    [SerializeField] private int moveDist;
    [HideInInspector] public int _moveDist;

    public float waitTime;

    private void Start()
    {
        _player = player;
        _moveDist = moveDist;
        _maxPlayerMoveCount = maxPlayerMoveCount;
        _minPlayerMoveCount = minPlayerMoveCount;
    }
}
