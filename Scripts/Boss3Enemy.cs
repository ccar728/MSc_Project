using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss3Enemy : EnemyBase
{
    public enum SkillType
    {
        Skill1, Skill2
    }
    public Vector2 skillIntervalRange = new Vector2(3, 4.5f);
    float skillInterval;
    float skillTimer = 0;
    SkillType[] skills;
    protected override void Start()
    {
        base.Start();
        health.takeDamageStartEvent += TakeDamageStartDefend;
        health.takeDamageStartEvent += (DamageMassage dm) =>
        {
            switch (dm.damageType)
            {
                case ElementType.None:
                    break;
                case ElementType.Fire:
                    {
                        dm.damage *= 2;
                    }
                    break;
                case ElementType.Ice:
                    {
                        
                    }
                    break;
                case ElementType.Lighting:
                    {

                    }
                    break;
                case ElementType.Wing:
                    {
                        dm.damage *= 2;
                    }
                    break;
                default:
                    break;
            }
        };
        //技能初始化
        skillInterval = UnityEngine.Random.Range(skillIntervalRange.x, skillIntervalRange.y);
        skills = Enum.GetValues(typeof(SkillType)) as SkillType[];
        defendShowGO.SetActive(false);
        StartCoroutine(DefendUseIE());
    }
    //被动技能 过段时间获得护盾
    IEnumerator DefendUseIE()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(8, 11));
            DefendUse();
        }
    }
    bool isDefend = false;
    void TakeDamageStartDefend(DamageMassage dm)
    {
        switch (dm.damageType)
        {
            case ElementType.None:
                break;
            case ElementType.Fire:
                {
                    dm.damage /=2;
                }
                break;
            case ElementType.Ice:
                {
                    
                }
                break;
            case ElementType.Lighting:
                break;
            case ElementType.Wing:
                {
                    dm.damage *= 2;
                }
                break;
            default:
                break;
        }
        if (isDefend)
        {
            dm.damage = 0;
        }
    }
    protected override void Die(DamageMassage dm)
    {
        base.Die(dm);
        MusicController.Instance.PlayEffectByFrame(Resources.Load<AudioClip>("Music/BossDie"));
    }
    float attackTimer = 0;
    public float attackInterval = 1.5f;
    public GameObject attackPrefabs;
    public float attackStartDistance;
    public float attackBulletMoveSpeed;
    //Transform playerTarget;
    protected override void Attack()
    {
        if (target == null)
        {
            return;
        }
        GameObject bullet = Instantiate(attackPrefabs);
        Vector2 dir = target.position - transform.position;
        //创造方向偏移
        dir.x = UnityEngine.Random.Range(dir.x - 0.15f, dir.x + 0.15f);
        dir.y = UnityEngine.Random.Range(dir.y - 0.15f, dir.y + 0.15f);
        bullet.transform.position = transform.position + new Vector3(dir.x, dir.y, 0).normalized * attackStartDistance;
        bullet.transform.right = dir;
        bullet.GetComponent<Rigidbody2D>().velocity = dir * attackBulletMoveSpeed;
        bullet.GetComponent<BulletBase>().attack = attack;
        bullet.GetComponent<BulletBase>().triggerTag = "Player";
    }

    protected override void AttackUpdate()
    {
        //if (!isMove)
        //{
        //    return;
        //}
        Debug.Log("普通攻击");
        attackTimer += Time.deltaTime;//普通攻击
        if (attackTimer >= attackInterval)
        {
            Attack();
            attackTimer %= attackInterval;
        }


        skillTimer += Time.deltaTime;

        if (skillTimer >= skillInterval)
        {
            skillInterval = UnityEngine.Random.Range(skillIntervalRange.x, skillIntervalRange.y);
            skillTimer %= skillInterval;
            int index = UnityEngine.Random.Range(0, skills.Length);
            switch (skills[index])
            {
                case SkillType.Skill1:
                    {
                        StartCoroutine(skill1IE());
                    }
                    break;
                case SkillType.Skill2:
                    {
                        StartCoroutine(skill1IE());
                    }
                    break;
                default:
                    break;
            }
            skillTimer = 0;
        }

    }



    public GameObject defendShowGO;
    public void DefendUse()
    {
        StartCoroutine(DefendIE());
    }
    IEnumerator DefendIE()
    {
        isDefend = true;
        defendShowGO.SetActive(true);
        yield return new WaitForSeconds(3);
        defendShowGO.SetActive(false);
        isDefend = false;
    }
    
 

    bool isMove = true;
    float noMoveTimer = 0;
    float noMoveInterval = 0.5f;
    System.Action noMoveEndEvent = null;
    protected override void MoveUpdate()
    {
        if (isMove)
        {
            base.MoveUpdate();
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            noMoveTimer += Time.deltaTime;
            if (noMoveTimer >= noMoveInterval)
            {
                noMoveTimer = 0;
                isMove = true;
                noMoveEndEvent?.Invoke();
                noMoveEndEvent = null;
            }
        }

    }
    public GameObject rainBulletPre;
    IEnumerator skill1IE()
    {
        CreateOneRain();
        yield return new WaitForSeconds(0.5f);
        CreateOneRain();
        yield return new WaitForSeconds(0.5f);
        CreateOneRain();
        yield return new WaitForSeconds(0.5f);
        CreateOneRain();
        //yield return new WaitForSeconds(0.5f);

    }
    public int skill1Damage = 10;
    void CreateOneRain()
    {
        //获取摄像机视野
        Bounds bound = Camera.main.transform.GetComponent<BoxCollider2D>().bounds;
        //总共三波随机下落雨点
        {
            for (int i = 0; i < 3; i++)
            {
                //第一波
                float x = UnityEngine.Random.Range(bound.min.x + 0.5f, bound.max.x - 0.5f);
                float y = UnityEngine.Random.Range(bound.min.y + 0.5f, bound.max.y - 0.5f);
                GameObject rain=Instantiate(rainBulletPre);
                rain.GetComponent<RainBullet>().Init(new Vector3(x,y,0));
                rain.GetComponent<RainBullet>().targetStr = "Player";
                rain.GetComponent<RainBullet>().attack = (int)(skill1Damage);
            }
        }
    }
}
