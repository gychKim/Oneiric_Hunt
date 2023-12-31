using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ObjectType
{
    None,
    NPC,
    Door,
    Object,
}

public class Object : MonoBehaviour
{
    public Action<bool> _checkPlayerEvt = null;

    [SerializeField] private QuestMarkUI _questMarkUI; // 퀘스트 이펙트
    [SerializeField] private TargetUIObj _targetObj;
    [SerializeField] private OutlineScript _outline;


    //[SerializeField] private Button _interactUI;
    public int _objID; // 본인의 ID
    
    bool _isActive = false;
    private void Awake()
    {
        
    }
    void Start()
    {
        QuestManager._instance._questMarkEvt -= SetQuestMark;
        QuestManager._instance._questMarkEvt += SetQuestMark;

        //_interactUI.onClick.AddListener(CheckObj);

        _targetObj.Init(_objID);
    }

    void Update()
    {
        if(_isActive)
        {
            if (Input.GetKeyDown(KeyCode.Space)|| SimpleInput.GetButtonDown("Space")) // Sapce를 누를때
            {
                CheckObj();
            }
        }
    }
    void CheckObj()
    {
        if (QuestManager._instance.QuestTrigger(_objID)) // 플레이어의 퀘스트에 본인이 포함되어 있으면
        {
            gameObject.SetActive(false); // 비활성화 시킨다.
        }
    }

    void SetQuestMark(QuestMark mark, int id)
    {
        if (id != _objID) return; // 본인의 ID와 다르면 리턴

        _questMarkUI.SetQuestMark(mark);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isActive = true;
            _checkPlayerEvt?.Invoke(true); // 플레이어가 들어왔으니, true를 전달
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isActive = false;
            _checkPlayerEvt?.Invoke(false); // 플레이어가 나갔으니, false를 전달
        }
    }
    private void OnDestroy()
    {
        QuestManager._instance._questMarkEvt -= SetQuestMark;
    }
}
