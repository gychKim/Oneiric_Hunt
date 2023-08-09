using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public List<Image> _skillFrontImgLst; // Amount를 1 => 0으로 수정시켜 쿨타임처럼 만든다.

    public List<GameObject> _weaponSkillLst; // 무기 변경시 해당 무기 스킬UI만를 보이도록 활성,비활성화 함
    void Start()
    {
        UIManager._instacne._skillEvt -= StartCoolTime;
        UIManager._instacne._skillEvt += StartCoolTime;

        UIManager._instacne._weaponEvt -= ChangeWeapon;
        UIManager._instacne._weaponEvt += ChangeWeapon;
    }

    void Update()
    {
        
    }
    void StartCoolTime(SkillScriptable scriptable, SkillManager.Skills skill) // 스크립터블 객체와, Skill Enum을 이용하여 스킬UI 쿨타임 구현
    {
        if (scriptable == null) return;

        SkillScriptable temp = null;

        temp = scriptable;

        Image img = _skillFrontImgLst[(int)skill];

        img.fillAmount = 1f; // 1로 해서, 스킬을 쿨타임 중 처럼 만듬

        if (temp != null)
            StartCoroutine(UpdateCoolUI(temp, img));

    }
    IEnumerator UpdateCoolUI(SkillScriptable scriptable, Image img)
    {
        while (scriptable._remainTime > 0)
        {
            img.fillAmount = scriptable._remainTime / scriptable._skillCoolTime;
            yield return new WaitForEndOfFrame();
        }
        img.fillAmount = 0f; // 쿨타임 끝나면 0으로 초기화 = 혹시나 모르니까
    }

    void ChangeWeapon(WeaponType weapon)
    {
        for(int i = 0; i < (int)WeaponType.Max; i++)
        {
            if(i == (int)weapon)
                _weaponSkillLst[i].SetActive(true);
            else
                _weaponSkillLst[i].SetActive(false);
        }
    }
    private void OnDestroy()
    {
        UIManager._instacne._skillEvt -= StartCoolTime;
    }
}
