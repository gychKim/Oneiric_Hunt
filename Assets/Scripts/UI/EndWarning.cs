using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWarning : MonoBehaviour
{
    public void ClickAgreeBtn() // 예 버튼
    {
        SoundManager._instance.PlayUISound();
        Application.Quit();
    }
    public void ClickDisAgreeBtn() // 아니오 버튼
    {
        SoundManager._instance.PlayUISound();
        UIManager._instacne.ClosePopupUI(); // UI매니저가 자신을 닫아줌 => 왜냐? 최근에 열린 PopupUI는 항상 자기자신이므로
    }
}
