using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public enum PlayState // ������ ���� ����
    {
        None,
        Dream_Normal,
        Dream_Battle,
        Real_Normal,
        Real_Chase,
        Real_Detect,
        MiniGame,
    }

    public PlayState Playstate { get { return _playState; } set { _playState = value; } }

    private PlayState _playState;

    public GameObject Player { get { return _player; } }

    private GameObject _player;
    public bool ChasePlayerDie { get { return _isChasePlayerDie; } set { _isChasePlayerDie = value; } }
    public bool PlayerDie { get { return _isPlayerDie; } set { _isPlayerDie = value; } }
    
    private bool _isPlayerDie= false;

    private bool _isChasePlayerDie = false;

    private void Awake()
    {
        _instance = this;

        _player = GameObject.FindGameObjectWithTag("Player"); // Scene ��ȯ ��, Player �±׸� ã��, ������ ����Ѵ�.
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    private void OnDestroy()
    {
        _isPlayerDie = false;
        _isChasePlayerDie = false;
    }
}