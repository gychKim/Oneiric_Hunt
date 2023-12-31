using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat : MonoBehaviour
{
    public enum TypeEnum
    {
        None = -1,
        Player,
        Enemy,
        Boss,
    }

    public TypeEnum Type
    {
        get { return _type; }
        set { _type = value; }
    }

    private TypeEnum _type = TypeEnum.None;

    public abstract float GetDamage(); // 누군가에게 데미지를 줄 때

    public abstract void SetDamage(float value); // 누군가로부터 데미지를 받을 때

    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    [SerializeField]
    private float _maxHp;
    public float HP { get { return _hp; } set { _hp = value; } }
    [SerializeField]
    private float _hp;

    public float MaxMp { get { return _maxMp; } set { _maxMp = value; } }
    [SerializeField]
    private float _maxMp;

    public float MP { get { return _mp; } set { _mp = value; } }
    [SerializeField]
    private float _mp;

    public float MinAtk { get { return _minAtk; } set { _minAtk = value; } }
    [SerializeField]
    private float _minAtk;

    public float MaxAtk { get { return _maxAtk; } set { _maxAtk = value; } }
    [SerializeField]
    private float _maxAtk;

    public float Defense { get { return _defense; } set { _defense = value; } }
    [SerializeField]
    private float _defense;

    public float MoveSpd { get { return _moveSpd; } set { _moveSpd = value; } }
    [SerializeField]
    private float _moveSpd;

}
