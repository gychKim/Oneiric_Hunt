using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QTEUI : MonoBehaviour
{
    public RectTransform _qteUI;
    public TextMeshProUGUI _qteText;
    void Start()
    {
        UIManager._instacne._qtePosEvt -= SetUIPos;
        UIManager._instacne._qtePosEvt += SetUIPos;
    }
    /*
    private void OnEnable() // 활성화 될 시, QTEUI를 1. 랜덤한 위치에 생성시킨다. 2. 지정된 위치에 생성시킨다.
    {
        
    }
    */
    void SetUIPos(Vector2 vec)
    {
        // 위치를 받은 후, _qteUI를 해당 위치로 보낸다.
        _qteUI.anchoredPosition = vec;
    }
}
