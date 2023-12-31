using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    //public ObjectInteractUI _interactionUI; // 상호작용 UI
    public ObjectType ObjType { get { return _objType; } set { _objType = value; } }

    [SerializeField] private ObjectType _objType = ObjectType.None;

    //[SerializeField] private Vector3 _playerPos;
    //[SerializeField] private Vector3 _pos;

    //[SerializeField] private RectTransform _interactUIRect; // UI의 Rect
    void Start()
    {
        //_interactionUI.Init(InteractionManager._instance.SetUIText(ObjType));
        //_interactUIRect = _interactionUI.GetComponent<RectTransform>();
        //_interactionUI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) // 1. 처음 들어오면 판단 => 선택된 아이템이 없으면 선택, 이미 존재해도 본인이 가장 가깝지 않을 것 이므로 넘김
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager._instacne.SetInteractBtnEvt(ObjType);
            /*
            _playerPos = other.transform.position; // 플레이어의 위치
            _pos = transform.position;//  객체의 위치


            Vector3 dir = _playerPos - _pos;
            dir.Normalize();


            float dis = Vector3.Distance(_playerPos, _pos);
            InteractionManager._instance.UpdateClosestInteractObj(this, dis);

            //Vector3 temp = new Vector3((_playerPos.x + _Pos.x) / 2, 2.5f, (_playerPos.z + _Pos.z) / 2); // y는 고정시키고, 플레이어와 객체 사이의 중앙값을 가져온다.

            Vector3 temp = new Vector3(_pos.x + (dir.x * 0.1f) , 3f, _pos.z + dir.z);

            _interactUIRect.position = temp;

            _interactionUI.gameObject.SetActive(true);*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager._instacne.SetInteractBtnEvt(ObjectType.None); // 빠져 나오면, 초기화.
            //_interactionUI.gameObject.SetActive(false);
            //InteractionManager._instance.ExitClosestInteractObj(this);
        }
    }
}
