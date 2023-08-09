using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager _instance;

    public DialogueData _nowData; // ���� ��ȭ ������ => �ʿ�����? 1. ��ȭ�����ϴ� NPC����Ʈ 2. ��� ����Ʈ 3. NPC���� ���ϴ� ��� index 4. �������������? 

    public Action<DialogueData> _dialogueEvt = null;

    public Action _clickNext = null; // ���� ��� ����ض�.

    QuestData _quest;
    void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        if (_nowData == null) return; //���� ��ϵ� ���ϰ� ������ �ٷ� ���϶���

        if(_clickNext != null && _nowData._isStart)
        {
            if(Input.GetKeyDown(KeyCode.Space) && !_nowData._isFinish)
            {
                _clickNext.Invoke();
            }
        }
    }
    
    public void GetDialogueLine(DialogueData data, QuestData quest = null) // ���̾�α� �����͸� �����´�.
    {
        _nowData = data;
        _quest = quest;

        if (_dialogueEvt != null)
        {
            _dialogueEvt.Invoke(data); // ������.
        }
    }
    public void EndDialogue()
    {
        switch (_nowData._dialogueType)
        {
            case DialogueType.QuestStart:
                QuestManager._instance.StartQuest(_quest);
                break;
            case DialogueType.QuestEnd:
                QuestManager._instance.FinishQuest(_quest);
                break;
        }
        _nowData = null;
    }
}