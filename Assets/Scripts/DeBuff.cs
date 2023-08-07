using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeBuff : MonoBehaviour
{
    float _downSwordMinAtk; // ���ҵ� �� �ּҰ��ݷ�
    float _downSwordMaxAtk; // ���ҵ� �� �ּҰ��ݷ�

    float _downSpearMinAtk;
    float _downSpearMaxAtk;

    float _downAxeMinAtk;
    float _downAxeMaxAtk;

    float _downDefense; // ���ҵ� ����

    float _downMonsterMinAtk;
    float _downMonsterMaxAtk;

    float _downMonsterDefense;

    float _downBossMinAtk;
    float _downBossMaxAtk;

    float _downBossDefense;

    float _downPlayerMovSpd;
    float _downMonsterMovSpd;
    float _downBossMovSpd;

    private PlayerStat _playerStat;
    private MonsterStat _monsterStat;
    private BossStat _bossStat;

    private float _duration;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region ���ݷ�
    public void StartAtkDeBuff(GameObject target, float value, float time)
    {
        FindStat(target);
        gameObject.name = "AtkDeBuff";
        _duration = time;
        if (_playerStat != null)
        {
            StartCoroutine(StartPlayerAtkDeBuffCo(value));
        }
        else if(_monsterStat != null)
        {
            StartCoroutine(StartMonsterAtkDeBuffCo(value));
        }
        else
        {
            StartCoroutine(StartBossAtkDeBuffCo(value));
        }
    }
    IEnumerator StartPlayerAtkDeBuffCo(float value)
    {
        DownPlayerAtk(value); // value��ŭ �÷��̾� ���ݷ� ����

        yield return new WaitForSeconds(_duration); // ���� �ð���ŭ ��� ��,

        ReturnPlayerAtk(); // ������� ����

        Destroy(gameObject);
    }
    IEnumerator StartMonsterAtkDeBuffCo(float value)
    {
        DownMonsterAtk(value);

        yield return new WaitForSeconds(_duration);

        ReturnMonsterAtk();

        Destroy(gameObject);
    }
    IEnumerator StartBossAtkDeBuffCo(float value)
    {
        DownBossAtk(value);

        yield return new WaitForSeconds(_duration);

        ReturnBossAtk();

        Destroy(gameObject);
    }

    void DownPlayerAtk(float value)
    {
        _downSwordMinAtk = _playerStat.SwordMinAtk * value;
        _downSwordMaxAtk = _playerStat.SwordMaxAtk * value;

        _downSpearMinAtk = _playerStat.SpearMinAtk * value;
        _downSpearMaxAtk = _playerStat.SpearMaxAtk * value;

        _downAxeMinAtk = _playerStat.AxeMinAtk * value;
        _downAxeMaxAtk = _playerStat.AxeMaxAtk * value;

        _playerStat.SwordMinAtk -= _downSwordMinAtk;
        _playerStat.SwordMaxAtk -= _downSwordMaxAtk;

        _playerStat.SpearMinAtk -= _downSpearMinAtk;
        _playerStat.SpearMaxAtk -= _downSpearMaxAtk;

        _playerStat.AxeMinAtk -= _downAxeMinAtk;
        _playerStat.AxeMaxAtk -= _downAxeMaxAtk;
    }
    void DownMonsterAtk(float value)
    {
        _downMonsterMinAtk = _monsterStat.MinAtk * value;
        _downMonsterMaxAtk = _monsterStat.MaxAtk * value;

        _monsterStat.MinAtk -= _downMonsterMinAtk;
        _monsterStat.MaxAtk -= _downMonsterMaxAtk;
    }
    void DownBossAtk(float value)
    {
        _downBossMinAtk = _bossStat.MinAtk * value;
        _downBossMaxAtk = _bossStat.MaxAtk * value;

        _bossStat.MinAtk -= _downBossMinAtk;
        _bossStat.MaxAtk -= _downBossMaxAtk;
    }
    void ReturnPlayerAtk()
    {
        _playerStat.SwordMinAtk += _downSwordMinAtk;
        _playerStat.SwordMaxAtk += _downSwordMaxAtk;

        _playerStat.SpearMinAtk += _downSpearMinAtk;
        _playerStat.SpearMaxAtk += _downSpearMaxAtk;

        _playerStat.AxeMinAtk += _downAxeMinAtk;
        _playerStat.AxeMaxAtk += _downAxeMaxAtk;
    }
    void ReturnMonsterAtk()
    {
        _monsterStat.MinAtk += _downMonsterMinAtk;
        _monsterStat.MaxAtk += _downMonsterMaxAtk;
    }
    void ReturnBossAtk()
    {
        _bossStat.MinAtk += _downBossMinAtk;
        _bossStat.MaxAtk += _downBossMaxAtk;
    }
    #endregion

    #region ����
    public void StartDefDeBuff(GameObject target, float value, float time)
    {
        FindStat(target);
        gameObject.name = "DefDeBuff";
        _duration = time;
        if (_playerStat != null)
        {
            StartCoroutine(StartPlayerDefDeBuffCo(value));
        }
        else if (_monsterStat != null)
        {
            StartCoroutine(StartMonsterDefDeBuffCo(value));
        }
        else
        {
            StartCoroutine(StartBossDefDeBuffCo(value));
        }
    }
    IEnumerator StartPlayerDefDeBuffCo(float value)
    {
        DownPlayerDef(value);

        yield return new WaitForSeconds(_duration); // ���� �ð���ŭ ��� ��,

        ReturnPlayerDef();

        Destroy(gameObject);
    }
    IEnumerator StartMonsterDefDeBuffCo(float value)
    {
        DownMonsterDef(value);

        yield return new WaitForSeconds(_duration);

        ReturnMonsterDef();

        Destroy(gameObject);
    }
    IEnumerator StartBossDefDeBuffCo(float value)
    {
        DownBossDef(value);

        yield return new WaitForSeconds(_duration);

        ReturnBossDef();

        Destroy(gameObject);
    }

    void DownPlayerDef(float value)
    {
        _downDefense = _playerStat.Defense * value;

        _playerStat.Defense -= _downDefense;
    }
    void DownMonsterDef(float value)
    {
        _downMonsterDefense = _monsterStat.Defense * value;

        _monsterStat.Defense -= _downMonsterDefense;
    }
    void DownBossDef(float value)
    {
        _downBossDefense = _bossStat.Defense * value;

        _bossStat.Defense -= _downBossDefense;
    }
    void ReturnPlayerDef()
    {
        _playerStat.Defense += _downDefense;
    }
    void ReturnMonsterDef()
    {
        _monsterStat.Defense += _downMonsterDefense;
    }
    void ReturnBossDef()
    {
        _bossStat.Defense += _downBossDefense;
    }

    #endregion

    #region �̵��ӵ�
    public void StartMovSpdDeBuff(GameObject target, float value, float time)
    {
        FindStat(target);
        gameObject.name = "MovSpdDeBuff";
        _duration = time;
        if (_playerStat != null)
        {
            StartCoroutine(StartPlayerMovSpdDeBuffCo(value));
        }
        else if (_monsterStat != null)
        {
            StartCoroutine(StartMonsterMovSpdDeBuffCo(value));
        }
        else
        {
            StartCoroutine(StartBossMovSpdDeBuffCo(value));
        }
    }
    IEnumerator StartPlayerMovSpdDeBuffCo(float value)
    {
        DownPlayerMovSpd(value);

        yield return new WaitForSeconds(_duration); // ���� �ð���ŭ ��� ��,

        ReturnPlayerMovSpd();

        Destroy(gameObject);
    }
    IEnumerator StartMonsterMovSpdDeBuffCo(float value)
    {
        DownMonsterMovSpd(value);

        yield return new WaitForSeconds(_duration);

        ReturnMonsterMovSpd();

        Destroy(gameObject);
    }
    IEnumerator StartBossMovSpdDeBuffCo(float value)
    {
        DownBossMovSpd(value);

        yield return new WaitForSeconds(_duration);

        ReturnBossMovSpd();

        Destroy(gameObject);
    }

    void DownPlayerMovSpd(float value)
    {
        _downPlayerMovSpd = _playerStat.MoveSpd * value;

        _playerStat.MoveSpd -= _downPlayerMovSpd;
    }
    void DownMonsterMovSpd(float value)
    {
        _downMonsterMovSpd = _monsterStat.MoveSpd * value;

        _monsterStat.MoveSpd -= _downMonsterMovSpd;
    }
    void DownBossMovSpd(float value)
    {
        _downBossMovSpd = _bossStat.MoveSpd * value;

        _bossStat.MoveSpd -= _downBossMovSpd;
    }
    void ReturnPlayerMovSpd()
    {
        _playerStat.MoveSpd += _downPlayerMovSpd;
    }
    void ReturnMonsterMovSpd()
    {
        _monsterStat.MoveSpd += _downMonsterMovSpd;
    }
    void ReturnBossMovSpd()
    {
        _bossStat.MoveSpd += _downBossMovSpd;
    }
    #endregion

    void FindStat(GameObject target) // Statã��
    {
        _playerStat = target.GetComponent<PlayerStat>();
        _monsterStat = target.GetComponent<MonsterStat>();
        _bossStat = target.GetComponent<BossStat>();
    }
}