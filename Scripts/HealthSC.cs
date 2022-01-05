using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSC : MonoBehaviour
{
    [HideInInspector]
    public int curHP;
    public int maxHP=10;
    public bool isDie = false;
    private void Start()
    {
        curHP=maxHP;
    }
    public delegate void DamageDelegate(DamageMassage damageMassage);
    public event DamageDelegate takeDamageEndEvent;
    public event DamageDelegate takeDamageStartEvent;
    public event DamageDelegate dieEvent;
    public void TakeDamage(DamageMassage dm)
    {
        if (isDie)
        {
            return;
        }
        takeDamageStartEvent?.Invoke(dm);
        curHP -= dm.damage;
        if (curHP<=0)
        {
            curHP = 0;
            
            Die(dm);
        }
        takeDamageEndEvent?.Invoke(dm);
    }
    public delegate void ReplayHPDelegate();
    public event ReplayHPDelegate replayCount;
    public void AddHP(int hp)
    {
        curHP += hp;
        if (curHP>maxHP)
        {
            curHP = maxHP;
        }

        replayCount?.Invoke();
    }
    void Die(DamageMassage dm)
    {
        isDie = true;
        dieEvent?.Invoke(dm);
    }
}
public class DamageMassage
{
    public int damage;
    public ElementType damageType;
    public DamageMassage(int damage,ElementType elemetType=ElementType.Fire)
    {
        this.damage = damage;
        this.damageType = elemetType;
    }
}
