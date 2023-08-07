using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill : MonoBehaviour
{
    SkillScriptable _scriptable;

    GameObject _player;

    Collider[] _monsterColls; // ������̴� ���� ��
    Collider[] _monsterDamageColls; // �������� ������ ���� ��

    HashSet<GameObject> _damagedTargets = new HashSet<GameObject>(); // ��ų�� �ѹ� �´��� ���� �ٷ��.

    float _startTime; // ���۽ð�
    float _duringTime; // ���ӽð�

    float _attractAmount; // ������̴� ����
    float _dmgAmount; // �������� ������ ����

    float _dmgStart; // ������ �ð����� start
    float _dmgDelay; // ������ �ִ� ������ �ð�

    float _atk; // ���� ��ų ���ݷ�

    float _buffDuringTime; // �̼Ӱ��� ����� ���� �ð�
    float _downMovSpdValue; // �̼Ӱ��� ����� ����

    int _layerMask = (1 << 7) | (1 << 10); // ���Ϳ� ������ Layer�� üũ
    public void Init(SkillScriptable scriptable, float playerAtk, Transform parent = null)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        _player = parent.gameObject;

        _atk = playerAtk * _scriptable._damageValue;
        _attractAmount = _scriptable._skillAmount;
        _dmgAmount = _scriptable._damageAmount;

        _dmgDelay = _scriptable._damageDelay;

        _duringTime = _scriptable._durationTime;

        _buffDuringTime = _scriptable._buffDurationTime;
        _downMovSpdValue = _scriptable._movSpdDeBuffValue;
    }
    void Start()
    {
        _startTime = Time.time; // ��ų ������ڸ��� ��ȯ�ǹǷ�, �ٷ� �ð�üũ
        _dmgStart = Time.time;

        BuffManager._instance.StartMovSpdDeBuff(_player, _downMovSpdValue, _buffDuringTime); // �̼� ���� ����
    }
    void FixedUpdate()
    {
        if (Time.time - _startTime <= _duringTime)
        {
            _monsterColls = Physics.OverlapSphere(transform.position, _attractAmount, _layerMask); // Layer�� Monster�� ������Ʈ���� Collider�� _monsterColls�� ��´�.
            _monsterDamageColls = Physics.OverlapSphere(transform.position, _dmgAmount, _layerMask);

            foreach (Collider coll in _monsterColls)
            {
                Vector3 dir = transform.position - coll.transform.position;
                coll.transform.position += dir * 1f * Time.deltaTime;
            }

            foreach (Collider coll in _monsterDamageColls)
            {
                if (!_damagedTargets.Contains(coll.gameObject))
                {
                    MonsterStat monsterStat = coll.GetComponent<MonsterStat>();
                    BossStat bossStat = coll.GetComponent<BossStat>();

                    if (monsterStat != null)
                        monsterStat.SetDamage(_atk);
                    else
                        bossStat.SetDamage(_atk);

                    _damagedTargets.Add(coll.gameObject);
                }
            }

            if (Time.time - _dmgStart >= _dmgDelay) // ������ ������ �ð��� ������ ����, ���� �� ����Ʈ �ʱ�ȭ
            {
                _dmgStart = Time.time;
                _damagedTargets.Clear();
            }
        }
        else
            Destroy(gameObject);
    }
}