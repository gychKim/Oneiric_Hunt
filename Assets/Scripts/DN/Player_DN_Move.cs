using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DN_Move : MonoBehaviour
{
    public Vector3 _dir;

    private PlayerStat _stat;
    private Player_DN_Anim _anim;
    private Player_DN_State _state;
    private Transform _cameTrans;

    public float _rotSpd;
    public float _h, _v;

    private float _magnitude;

    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _state = GetComponent<Player_DN_State>();
        _anim = GetComponent<Player_DN_Anim>();

        _cameTrans = Camera.main.transform;
    }
    void Start()
    {

    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (GameManager._instance.PlayerDie) // ��ų�ε�, Move��ų�� ��� ���̶�� ����
            return;

        GetDir();

        switch (_state.PlayerState)
        {
            case Player_DN_State.DN_State.Idle:
                UpdateIdle();
                break;

            case Player_DN_State.DN_State.Run:
            case Player_DN_State.DN_State.Walk:
                UpdateMove();
                break;

            case Player_DN_State.DN_State.Check:
                UpdateCheck();
                break;
        }

        Rotate();
    }
    void GetDir()
    {
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");

        _dir = new Vector3(_h, 0, _v);
        _dir = Quaternion.AngleAxis(_cameTrans.rotation.eulerAngles.y, Vector3.up) * _dir; // ���� _dir * y�� �������� ī�޶��� rotation.y����ŭ Quaternion�� �����Ѵ�.

        _dir = _dir.normalized;

        _magnitude = Mathf.Clamp01(_dir.magnitude) * _stat.MoveSpd; // �������߸� �ӵ��� �ش�.
    }
    private void UpdateIdle()
    {
        if(_magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _state.PlayerState = Player_DN_State.DN_State.Walk;
            }
            else
            {
                _state.PlayerState = Player_DN_State.DN_State.Run;
            }
            _anim.CrossFade(_state.PlayerState);
        }
    }
    private void UpdateMove()
    {
        if (_magnitude <= 0)
        {
            _state.PlayerState = Player_DN_State.DN_State.Idle;
            _anim.CrossFade(_state.PlayerState);
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _magnitude /= 2f;
            _state.PlayerState = Player_DN_State.DN_State.Walk;
        }
        else
        {
            _state.PlayerState = Player_DN_State.DN_State.Run;
        }

        _anim.CrossFade(_state.PlayerState);

        transform.position += _dir * _magnitude * Time.deltaTime;
    }
    private void UpdateCheck()
    {

    }
    void Rotate()
    {
        if (_dir != Vector3.zero) // _dir�� 0�� �ƴ϶��, ��! �����̰� �ִٸ�,
        {
            Quaternion quat = Quaternion.LookRotation(_dir, Vector3.up); // ù��° ���ڴ� �ٶ󺸴� �����̸�, �ι�° ���ڴ� ���̴�. => ù��° ���ڴ� �ٶ󺸰��� �ϴ� ���⺤�Ͱ� �����Ѵ�.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, _rotSpd * Time.deltaTime); // (ù��°) ���� (�ι�°)���� (����°)�� �ӵ��� ȸ���� ����� �����Ѵ�.
        }
    }
}