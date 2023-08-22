using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaseMovement : MonoBehaviour
{
    //public Vector3 Dir { get { return _dir; } set { _dir = value; } }

    public float _rotSpd;
    public float _h, _v;

    private PlayerStat _stat;
    public Vector3 _dir;

    [SerializeField]
    private Transform _cameTrans;

    //CharacterController _contrl;
    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (ChaseManager._instance.ChasePlayerDie) return;

        Move();
        Rotate();
    }
    void Move()
    {
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");

        _dir = new Vector3(_h, 0, _v);
        _dir = Quaternion.AngleAxis(_cameTrans.rotation.eulerAngles.y, Vector3.up) * _dir; // 기존 _dir * y축 기준으로 카메라의 rotation.y값만큼 Quaternion을 리턴한다.

        _dir = _dir.normalized;

        //transform.Translate(_dir * _moveSpd * Time.deltaTime);
        float magnitude = Mathf.Clamp01(_dir.magnitude) * _stat.MoveSpd; // 움직여야만 속도를 준다.

        transform.position += _dir * magnitude * Time.deltaTime;
        //_contrl.SimpleMove(_dir * magnitude * Time.deltaTime);
    }
    void Rotate()
    {
        if (_dir != Vector3.zero) // _dir이 0이 아니라면, 즉! 움직이고 있다면,
        {
            Quaternion quat = Quaternion.LookRotation(_dir, Vector3.up); // 첫번째 인자는 바라보는 방향이며, 두번째 인자는 축이다. => 첫번째 인자는 바라보고자 하는 방향벡터가 들어가야한다.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, _rotSpd * Time.deltaTime); // (첫번째) 에서 (두번째)까지 (세번째)의 속도로 회전한 결과를 리턴한다.
        }

        /*
        float angle = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, angle, 0f);*/
    }
    private void OnApplicationFocus(bool focus) // 게임 중, 게임을 포커스 받으면, 실행
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
