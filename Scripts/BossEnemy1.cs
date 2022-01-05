using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossEnemy1 : EnemyBase
{
    public enum SkillType
    {
        Skill1,Skill2
    }
    public Vector2 skillIntervalRange=new Vector2(3,4.5f);
    float skillInterval;
    float skillTimer = 0;
    SkillType[] skills;
    protected override void Start()
    {
        base.Start();
        health.takeDamageStartEvent += (DamageMassage dm) =>
        {
            switch (dm.damageType)
            {
                case ElementType.None:
                    break;
                case ElementType.Fire:
                    {
                        
                    }
                    break;
                case ElementType.Ice:
                    {
                        dm.damage *= 2;
                    }
                    break;
                case ElementType.Lighting:
                    {
                        dm.damage *= 3;
                    }
                    break;
                case ElementType.Wing:
                    {
                        dm.damage /= 2;
                    }
                    break;
                default:
                    break;
            }
        };
      //技能初始化
      skillInterval = UnityEngine.Random.Range(skillIntervalRange.x, skillIntervalRange.y);
        skills = Enum.GetValues(typeof(SkillType)) as SkillType[];
    }
    protected override void Die(DamageMassage dm)
    {
        base.Die(dm);
        MusicController.Instance.PlayEffectByFrame(Resources.Load<AudioClip>("Music/BossDie"));
    }
    protected override void AttackUpdate()
    {
        if (!isMove)
        {
            return;
        }
      //  Debug.Log("攻击计时");
        // base.AttackUpdate();
        skillTimer += Time.deltaTime;

        if (skillTimer>= skillInterval)
        {
            skillInterval = UnityEngine.Random.Range(skillIntervalRange.x, skillIntervalRange.y);
            skillTimer %= skillInterval;
            int index = UnityEngine.Random.Range(0, skills.Length);
            switch (skills[index])
            {
                case SkillType.Skill1:
                    {
                        Skill1Use();
                    }
                    break;
                case SkillType.Skill2:
                    {
                        Skill2Use();
                    }
                    break;
                default:
                    break;
            }
            skillTimer = 0;
        }
       
    }
    public GameObject bossBullet1Prefabs;
    public int bossBullet1Count = 6;
    public int bossBullet1Count2 = 5;
    public int bossBullet1Count3 = 4;
    public float bullet1MoveSpeed = 2;
    public float bullet1StartDistance = 0;
    public float skill1NoMoveTime = 0.5f;
    public void Skill1Use()
    {
        StartCoroutine(skill1IE());
    }
    public int skill1Damage = 15;
    IEnumerator skill1IE()
    {
        Debug.Log("释放技能1");
        {
            //环形子弹*6
            float oneAngle = 360 / bossBullet1Count;
            for (int i = 0; i < bossBullet1Count; i++)
            {
                GameObject bullet = Instantiate(bossBullet1Prefabs);
                Vector2 dir = new Vector2(Mathf.Cos(oneAngle * i / 180.0f * Mathf.PI), Mathf.Sin(oneAngle * i / 180.0f * Mathf.PI)).normalized;
                bullet.transform.position = transform.position + new Vector3(dir.x, dir.y, 0) * bullet1StartDistance;
                bullet.transform.right = dir;
                bullet.GetComponent<Rigidbody2D>().velocity = dir * bullet1MoveSpeed;
                bullet.GetComponent<BulletBase>().attack = skill1Damage;
                bullet.GetComponent<BulletBase>().triggerTag = "Player";
            }
            isMove = false;
            noMoveInterval = skill1NoMoveTime;
        }
        yield return new WaitForSeconds(0.5f);
        {
            float oneAngle = 360 / bossBullet1Count2;
            for (int i = 0; i < bossBullet1Count2; i++)
            {
                GameObject bullet = Instantiate(bossBullet1Prefabs);
                Vector2 dir = new Vector2(Mathf.Cos(oneAngle * i / 180.0f * Mathf.PI), Mathf.Sin(oneAngle * i / 180.0f * Mathf.PI)).normalized;
                bullet.transform.position = transform.position + new Vector3(dir.x, dir.y, 0) * bullet1StartDistance;
                bullet.transform.right = dir;
                bullet.GetComponent<Rigidbody2D>().velocity = dir * bullet1MoveSpeed;
                bullet.GetComponent<BulletBase>().attack = skill1Damage;
                bullet.GetComponent<BulletBase>().triggerTag = "Player";
            }
            isMove = false;
            noMoveInterval = skill1NoMoveTime;
        }
        yield return new WaitForSeconds(0.5f);
        {
            float oneAngle = 360 / bossBullet1Count3;
            for (int i = 0; i < bossBullet1Count3; i++)
            {
                GameObject bullet = Instantiate(bossBullet1Prefabs);
                Vector2 dir = new Vector2(Mathf.Cos(oneAngle * i / 180.0f * Mathf.PI), Mathf.Sin(oneAngle * i / 180.0f * Mathf.PI)).normalized;
                bullet.transform.position = transform.position + new Vector3(dir.x, dir.y, 0) * bullet1StartDistance;
                bullet.transform.right = dir;
                bullet.GetComponent<Rigidbody2D>().velocity = dir * bullet1MoveSpeed;
                bullet.GetComponent<BulletBase>().attack = skill1Damage;
                bullet.GetComponent<BulletBase>().triggerTag = "Player";
            }
            isMove = false;
            noMoveInterval = skill1NoMoveTime;
        }
    }
    public float skill2NoMoveStartTime=1.5f;
    public float skill2NoMoveEndTime = 0.5f;
    public GameObject boomEffectPre;
    public int skill2Damage = 40;
    public void Skill2Use()
    {
        Debug.Log("释放技能2");
        isMove = false;
        noMoveInterval = skill2NoMoveStartTime;
        noMoveEndEvent = () =>//蓄力结束爆炸
        {
            GameObject boom=Instantiate(boomEffectPre);
            boom.transform.position = transform.position;
            boom.GetComponent<BoomEffect>().attack= skill2Damage;
            boom.GetComponent<BoomEffect>().triggerTargetTag = "Player";
            isMove = false;
            noMoveInterval = skill2NoMoveEndTime;
        };
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
            if (noMoveTimer>=noMoveInterval)
            {
                noMoveTimer = 0;
                isMove = true;
                noMoveEndEvent?.Invoke();
                noMoveEndEvent = null;
            }
        }
        
    }
    
}
