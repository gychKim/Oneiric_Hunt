using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStat : Stat
{
    public EnemyCanvas _canvas;

    private bool _isDead;

    [SerializeField] int _id; // 몬스터 id

    Animator _anim;

    void Start()
    {
        Type = TypeEnum.Enemy;
        _anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public override float GetDamage() // 누군가에게 데미지를 줄 때
    {
        float dmg = Random.Range(MinAtk, MaxAtk);
        return dmg;
    }
    public override void SetDamage(float value)
    {
        if (_isDead) return; // 죽을 때 계속 2번 죽어서 조건 추가하여 버그 방지

        float dmg = value - Defense;

        HP -= dmg;

        _canvas.SetHPAmount(HP / MaxHp);

        if (HP <= 0)
        {
            _isDead = true;

            QuestManager._instance.QuestTrigger(_id);

            Destroy(gameObject);
        }
        else
        {
            _anim.CrossFade("Hit", 0.1f);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAtk"))
        {
            SetDamage(other.GetComponentInParent<PlayerStat>().GetDamage());
        }
    }
    private void OnDestroy()
    {
        
    }
}
