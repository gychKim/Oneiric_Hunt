using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Attack : MonoBehaviour
{
    private PlayerStat _stat;
    private Player_DB_Anim _anim;
    private Player_DB_Move _move;
    private Coroutine _atkCo;

    public bool _isAttack = false;
    private bool _ischeckAttack = false;

    private float _stopAtkTime;
    private float _startAtkDelay;
    private float _atkDelay = 0.8f;
    private float _idleTime = 2f;

    private bool _isStopAtk = true;
    private bool _isFirstAttack = false;
    private bool _isSecondAtk = false;
    private bool _isThirdAtk = false;

    void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _anim = GetComponent<Player_DB_Anim>();
        _move = GetComponent<Player_DB_Move>();
    }
    void Start()
    {

    }
    void Update()
    {
        if (GameManager._instance.PlayerDie || GameManager._instance.Playstate != GameManager.PlayState.Dream_Battle || _move._isMove || SkillManager._instance._isSkilling) return;

        if (_isStopAtk)
        {
            // 2초 동안 계속 가만히 있었으면
            if (Time.time - _stopAtkTime >= _idleTime)
            {
                Debug.Log("공격 초기화");
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false;
            }
        }

        if (Input.GetMouseButton(0))
        {
            _isAttack = true;

            if (_ischeckAttack) return;

            _isStopAtk = false;

            if (Input.GetMouseButton(0) && _isFirstAttack == false)
            {
                Debug.Log("첫번째 공격 시작");

                _anim.CrossFade(Player_DB_State.DB_State.Attack_1);
                _isFirstAttack = true;
            }
            else if (Input.GetMouseButton(0) && _isSecondAtk == false)
            {
                Debug.Log("두번째 공격 시작");

                _anim.CrossFade(Player_DB_State.DB_State.Attack_2);
                _isSecondAtk = true;
            }
            else if (Input.GetMouseButton(0) && _isThirdAtk == false)
            {
                Debug.Log("세번째 공격 시작");

                _anim.CrossFade(Player_DB_State.DB_State.Attack_3);
                _isThirdAtk = true;
            }
            else if (Input.GetMouseButton(0))
            {
                Debug.Log("네번째 공격 시작");

                _anim.CrossFade(Player_DB_State.DB_State.Attack_4);
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false; // 다시 첫번째 공격으로
            }

            if (_atkCo != null) // 사실 할 필요 없는 듯 한데 혹시나 모를 변수를 막기위해
                StopCoroutine(StartAttackDelayTime());
            StartCoroutine(StartAttackDelayTime());
        }
        else if (_isStopAtk == false) // 공격을 하지 않으면, 대기상태 시간측정 시작 // 마우스 좌클릭을 꾹 누르지 않고, 클릭을 계속 할시, 클릭을 땔 때 마다, 이 부분을 실행하여 _ischeckAttack이 false가 되어, 빠르게 다음 공격으로 넘어갈 수 있게 된 것.
        {
            _isAttack = false;
            _ischeckAttack = false;

            _stopAtkTime = Time.time;
            _isStopAtk = true;
        }
    }
    
    IEnumerator StartAttackDelayTime() // 공격 딜레이
    {
        _ischeckAttack = true;

        yield return new WaitForSeconds(_atkDelay);

        _ischeckAttack = false;
    }
    void FixedUpdate()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MonsterAtk"))
        {
            float dmg = other.GetComponentInParent<MonsterStat>().GetDamage();
            _stat.SetDamage(dmg);
        }
        else if (other.CompareTag("BossAtk"))
        {
            float dmg = other.GetComponentInParent<BossStat>().GetDamage();
            _stat.SetDamage(dmg);
        }
    }
}
