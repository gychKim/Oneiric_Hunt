using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour
{
    float _maxHP;

    public Image _hpImg;

    void Start()
    {
        UIManager._instacne._hpEvt -= SetUI;
        UIManager._instacne._hpEvt += SetUI;

        _maxHP = GameObject.FindWithTag("Player").GetComponent<PlayerStat>().MaxHp; // ������ �� �÷��̾ ã�� ��, �ִ�ü���� �����´�. => �ִ�ü���� ������ ����
    }

    void Update()
    {
        
    }
    void SetUI(float value) // �÷��̾��� HP�� ���� �߻�
    {
        _hpImg.fillAmount = value / _maxHP; // ����ü�� / �ִ�ü���� ������ _hpImg�� fillAmount�� �����Ų��.
    }
}