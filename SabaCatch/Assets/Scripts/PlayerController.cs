using System.Collections;
using System.Collections.Generic;
using falling;
using UnityEngine;
using falling;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Sprite normalFace;
    [SerializeField] private Sprite winFace;
    [SerializeField] private Sprite loseFace;

    [SerializeField] private SpriteRenderer _playerSprite;
    
    private int _maxCount;
    private int _minCount;

    private bool isWait;

    private void Start()
    {
        Debug.Log(_playerSprite.name);
        
        _maxCount = PlayerManager.Instance._maxPlayerMoveCount;
        _minCount = PlayerManager.Instance._minPlayerMoveCount;
        Debug.Log(_minCount+" "+_maxCount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerManager.Instance.playerMoveCount += 1;
            playerMoving();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerManager.Instance.playerMoveCount -= 1;
            playerMoving();
        }
        
        
        //test
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _playerSprite.sprite = loseFace;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            _playerSprite.sprite = winFace;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            _playerSprite.sprite = normalFace;
        }
        
    }


    private void playerMoving()
    {
        PlayerManager.Instance.playerMoveCount =
            Mathf.Clamp(PlayerManager.Instance.playerMoveCount, _minCount, _maxCount);
        
        
        gameObject.transform.position =
            new Vector3(PlayerManager.Instance.playerMoveCount * PlayerManager.Instance._moveDist, transform.position.y,
                transform.position.z);
        
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        FallingObject _falling = other.gameObject.GetComponent<FallingObject>();
        if (_falling == null)
        {
            Debug.Log("おっこちてきたオブジェクトにFallingObjectのスクリプトがくっついてないよ！");
        }
        else if (_falling._fallingObjectType == FallingObjectType.ObjectType.SABA)
        {
            Destroy(other.gameObject);
            changeSprite(FallingObjectType.ObjectType.SABA);
            ScoreManager.Instance.subtractionScore();
            Debug.Log("SABA");
        }
        else if (_falling._fallingObjectType == FallingObjectType.ObjectType.NOTSABA)
        {
            Destroy(other.gameObject);
            changeSprite(FallingObjectType.ObjectType.NOTSABA);
            ScoreManager.Instance.additionScore();
            Debug.Log("NOT_SABA");
        }
    }

    private void changeSprite(FallingObjectType.ObjectType type)
    {   
        if (type == FallingObjectType.ObjectType.SABA)
        {
            _playerSprite.sprite = loseFace;
            StartCoroutine(waitTime());
        }
        else
        {
            _playerSprite.sprite = winFace;
            StartCoroutine(waitTime());
        }
    }

    private IEnumerator waitTime()
    {
        yield return new WaitForSeconds(PlayerManager.Instance.waitTime);
        _playerSprite.sprite = normalFace;
        yield break;
    }
    
    
    
}
