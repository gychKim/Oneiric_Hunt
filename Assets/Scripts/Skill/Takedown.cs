using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takedown : MonoBehaviour
{
    SkillScriptable _scriptable;
    Collider[] _colls = new Collider[10];

    float _dmgAmount; // 스킬 범위
    float _durationTime;
    float _atk; // 최종 스킬 공격력

    int _layerMask = (1 << 7) | (1 << 10); // 몬스터와 보스의 Layer만 체크

    public void Init(SkillScriptable scriptable, float playerAtk)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        _atk = playerAtk * _scriptable._damageValue;

        _dmgAmount = _scriptable._damageAmount;
        _durationTime = _scriptable._durationTime;
    }

    void Start()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _dmgAmount, _colls, _layerMask);

        foreach (Collider coll in _colls)
        {
            if(coll)
            {
                Stat stat = coll.GetComponent<Stat>();

                if (stat != null)
                    stat.SetDamage(_atk);
            }
        }
        Destroy(gameObject, _durationTime);
    }
    private void OnDestroy()
    {
        SkillManager._instance.EndSkill();
    }
}
