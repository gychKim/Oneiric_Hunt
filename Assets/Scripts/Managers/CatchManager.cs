using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchManager : MonoBehaviour
{
    public static CatchManager _instance;

    public bool CheckCatchSuccess { get { return _isSuccess; } }

    private CatchEvent _eventData;
    private CatchPolice _police;
    
    private bool _clickQ = false;

    private float _maxGauge = 1f;
    private float _nowGauge; // ���� ������
    private float _clickGainGauge = 0.1f; // ��ư ���� �� ȹ���ϴ� ������ �� 

    private bool _isSuccess;
    private bool _isStart;
    //private bool _isFail; // ���� Ȯ�� ����
    private bool _isEnd; // �� Ȯ�� ����


    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (!_isStart) return;

        if(_maxGauge <= _nowGauge || _isSuccess)
        {
            CatchEventEnd();
        }
        else
        {
            CheckKey();
        }
        
    }
    public void StartCatchEvt(CatchEvent evt)
    {
        _eventData = evt;

        _isStart = true;
        _isEnd = false;
        
        _police = _eventData._police;
    }
    void CheckKey()
    {
        if (_police.State == CatchPolice.CatchPoliceState.Fight)
        {
            if (_clickQ)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _nowGauge += _clickGainGauge;
                    _clickQ = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    _nowGauge += _clickGainGauge;
                    _clickQ = true;
                }
            }

            Debug.Log("���� ������ �� : " + _nowGauge);

            if (_maxGauge <= _nowGauge)
            {
                _isSuccess = true;
            }
        }
    }
    void CatchEventEnd()
    {
        if(_isSuccess)
        {
            _isStart = false;
            _isEnd = true;
            _eventData = null;
            Debug.Log("��������");
        }
    }
}