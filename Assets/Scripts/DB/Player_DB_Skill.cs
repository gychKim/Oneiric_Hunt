using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Sword,
    Spear,
    Axe,
    Max,
}

public class Player_DB_Skill : MonoBehaviour
{
    // WeaponSwap��ų���� ChagneWeapon�̶�� �Լ��� ȣ���ؾ��Ѵ�
    // �� �Լ��� �÷��̾ ���ϰ� �ִ� ���⸦ Ȯ���ϰ�, ��ü�Ǵ� ����� �����ؾ� �ϴ� ���̴�.
    // �׸��� �ִϸ��̼ǵ� CrossFade�� ���ڷ� ���⸦ �־��ְ�, �ִϸ����͵� ������ ��� �Ѵ�.
    public WeaponType Weapon { get { return _weapon; } set { _weapon = value; } }

    public WeaponType _weapon = WeaponType.Sword;

    private Player_DB_Anim _anim;
    private PlayerStat _stat;
    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _anim = GetComponent<Player_DB_Anim>();
    }
    void Start()
    {
        UIManager._instacne.SetWeapon(Weapon); // ���� ���� ������ UI�Ŵ������� ����
    }
    void Update()
    {
        if (GameManager._instance.PlayerDie || SkillManager._instance._isSkilling || GameManager._instance.Playstate != GameManager.PlayState.Dream_Battle) return; // �÷��̾ �׾��ų�, ��ų ��� �� �̶�� ����
        // �켱 �̰ͺ��� ���δ�... ���� ������ ���� �����, �̺�Ʈ�� �ݹ�� �� ���� ������ ����Ű��� �ϴ°� ��������?

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkillManager._instance.StartSkill(SkillManager.Skills.Dodge, 0f, transform.position, transform.rotation, transform);
            _anim.CrossFade(Player_DB_State.DB_State.Skill, SkillManager.Skills.Dodge);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SkillManager._instance.StartSkill(SkillManager.Skills.WeaponSwap, 0f, transform.position, transform.rotation, transform);
            _anim.CrossFade(Player_DB_State.DB_State.Skill, SkillManager.Skills.WeaponSwap);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            float dmg;
            switch (Weapon)
            {
                case WeaponType.Sword:
                    dmg = Random.Range(_stat.SwordMinAtk, _stat.SwordMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Slash, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(Player_DB_State.DB_State.Skill, SkillManager.Skills.Slash);
                    break;
                case WeaponType.Spear:
                    dmg = Random.Range(_stat.SpearMinAtk, _stat.SpearMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Stabing, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(Player_DB_State.DB_State.Skill, SkillManager.Skills.Stabing);
                    break;
                case WeaponType.Axe:
                    dmg = Random.Range(_stat.AxeMinAtk, _stat.AxeMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Takedown, dmg, transform.position, transform.rotation);
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            float dmg;
            switch (Weapon)
            {
                case WeaponType.Sword:
                    dmg = Random.Range(_stat.SwordMinAtk, _stat.SwordMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.SwordForce, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(Player_DB_State.DB_State.Skill, SkillManager.Skills.SwordForce);
                    break;

                case WeaponType.Spear:
                    dmg = Random.Range(_stat.SpearMinAtk, _stat.SpearMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Sweep, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(Player_DB_State.DB_State.Skill, SkillManager.Skills.Sweep);
                    break;

                case WeaponType.Axe:
                    dmg = Random.Range(_stat.AxeMinAtk, _stat.AxeMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.WindMill, dmg, transform.position, transform.rotation, transform);
                    _anim.CrossFade(Player_DB_State.DB_State.Skill, SkillManager.Skills.WindMill);
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            float dmg;
            switch (Weapon)
            {
                case WeaponType.Sword:
                    dmg = Random.Range(_stat.SwordMinAtk, _stat.SwordMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.SpaceCut, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(Player_DB_State.DB_State.Skill, SkillManager.Skills.SpaceCut);
                    break;

                case WeaponType.Spear:
                    dmg = Random.Range(_stat.SpearMinAtk, _stat.SpearMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Challenge, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(Player_DB_State.DB_State.Skill, SkillManager.Skills.Challenge);
                    break;

                case WeaponType.Axe:
                    dmg = Random.Range(_stat.AxeMinAtk, _stat.AxeMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Berserk, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(Player_DB_State.DB_State.Skill, SkillManager.Skills.Berserk);
                    break;
            }
        }

    }
}