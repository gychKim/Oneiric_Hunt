using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStat _stat;
    public SphereCollider _hand; // 테스트 => 공격 Collider

    private float _stopAtkTime;
    private float _startAtkDelay;

    private float _idleTime = 2f;

    private bool _isStopAtk = true;
    private bool _isAttack = false;
    private bool _isFirstAttack = false;
    private bool _isSecondAtk = false;
    private bool _isThirdAtk = false;

    private WaitForSeconds _atkDelay = new WaitForSeconds(0.7f);
    void Awake()
    {
        _stat = GetComponent<PlayerStat>();
    }
    void Start()
    {

    }
    void Update()
    {
        if (GameManager._instance.PlayerDie || GameManager._instance.Playstate != GameManager.PlayState.Dream_Battle) return;

        if (_isStopAtk)
        {
            // 2초 동안 계속 가만히 있었으면
            if (Time.time - _stopAtkTime >= _idleTime)
            {
                Debug.Log("공격 초기화");
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_isAttack)
                return;

            /*
            if(_startAtkDelay == 0)
                _startAtkDelay = Time.time;

            if (Time.time - _startAtkDelay <= _atkDelay) return;

            if (_startAtkDelay != 0)
                _startAtkDelay = 0;
            */

            _hand.enabled = true;

            if (Input.GetMouseButtonDown(0) && _isFirstAttack == false)
            {
                Debug.Log("첫번째 공격 시작");
                _isStopAtk = false;
                _isFirstAttack = true;
            }
            else if (Input.GetMouseButtonDown(0) && _isSecondAtk == false)
            {
                Debug.Log("두번째 공격 시작");
                _isSecondAtk = true;
            }
            else if (Input.GetMouseButtonDown(0) && _isThirdAtk == false)
            {
                Debug.Log("세번째 공격 시작");
                _isThirdAtk = true;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("네번째 공격 시작");
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false; // 다시 첫번째 공격으로
            }
            StartCoroutine(StartAttackDelayTime());
        }
        else if (_isStopAtk == false) // 공격을 하지 않으면, 대기상태 시간측정 시작
        {
            _hand.enabled = false;
            _stopAtkTime = Time.time;
            _isStopAtk = true;
        }
    }
    IEnumerator StartAttackDelayTime() // 공격 딜레이
    {
        _isAttack = true;
        _hand.enabled = true;

        yield return _atkDelay;

        _hand.enabled = false;
        _isAttack = false;
    }
    void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MonsterAtk"))
        {
            float dmg = other.GetComponentInParent<MonsterStat>().GetDamage();
            _stat.SetDamage(dmg);
        }
        else if(other.CompareTag("BossAtk"))
        {
            float dmg = other.GetComponentInParent<BossStat>().GetDamage();
            _stat.SetDamage(dmg);
        }
    }
}
