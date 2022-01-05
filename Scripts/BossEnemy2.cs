using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossEnemy2 : EnemyBase
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
        health.takeDamageStartEvent += (DamageMassage dm) =>
        {
            switch (dm.damageType)
            {
                case ElementType.None:
                    break;
                case ElementType.Fire:
                    {
                        dm.damage /= 2;
                    }
                    break;
                case ElementType.Ice:
                    {
                        dm.damage *= 2;
                    }
                    break;
                case ElementType.Lighting:
                    {

                    }
                    break;
                case ElementType.Wing:
                    {
                       
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
    float attackTimer = 0;
    public float attackInterval = 1.5f;
    public GameObject attackPrefabs;
    public float attackStartDistance;
    public float attackBulletMoveSpeed;
    //Transform playerTarget;
    protected override void Attack()
    {
        if (target==null)
        {
            return;
        }
        GameObject bullet = Instantiate(attackPrefabs);
        Vector2 dir =target.position-transform.position;
        //创造方向偏移
        dir.x = UnityEngine.Random.Range(dir.x-0.15f, dir.x + 0.15f);
        dir.y = UnityEngine.Random.Range(dir.y - 0.15f, dir.y + 0.15f);
        bullet.transform.position = transform.position + new Vector3(dir.x, dir.y, 0).normalized * attackStartDistance;
        bullet.transform.right = dir;
        bullet.GetComponent<Rigidbody2D>().velocity = dir * attackBulletMoveSpeed;
        bullet.GetComponent<BulletBase>().attack = attack;
        bullet.GetComponent<BulletBase>().triggerTag = "Player";
    }
    protected override void AttackUpdate()
    {
        if (!isMove)
        {
            return;
        }
        attackTimer += Time.deltaTime;//普通攻击
        if (attackTimer>=attackInterval)
        {
            Attack();
            attackTimer %= attackInterval;
        }

        //  Debug.Log("攻击计时");
        // base.AttackUpdate();
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
   //public int bossBullet1Count = 6;
    public float bullet1MoveSpeed = 2;
    public float bullet1StartDistance = 0;
    public float skill1NoMoveTime = 0.5f;
    public float angleOffect = 30f;
    public void Skill1Use()
    {
        StartCoroutine(skill1IE());
    }
    public int skill1Damage = 10;
    IEnumerator skill1IE()
    {
        //对玩家发射三个子弹 
        if (target != null)
        {

            Vector2 dirT = target.position - transform.position;
            float curAngle = 0;
            if (dirT.x == 0)
            {
                if (dirT.y > 0)
                {
                    curAngle = 90;
                }
                else
                {
                    curAngle = -90;
                }
            }
            else
            {
                curAngle = Mathf.Atan(dirT.y / dirT.x) / Mathf.PI * 180;
                if (dirT.x < 0)
                {
                    curAngle += 180;
                }
            }
            Debug.Log("释放技能1" + "自身位置" + transform.position + "目标位置" + target.position + "目标角度" + curAngle);
            {//第一波
                for (int i = -1; i < 2; i++)
                {
                    GameObject bullet = Instantiate(bossBullet1Prefabs);
                    Vector2 dirM = new Vector2(Mathf.Cos((curAngle + angleOffect * i) / 180.0f * Mathf.PI), Mathf.Sin((curAngle + angleOffect * i) / 180.0f * Mathf.PI)).normalized;
                    bullet.transform.position = transform.position + new Vector3(dirM.x, dirM.y, 0) * bullet1StartDistance;
                    bullet.transform.right = dirM;
                    bullet.GetComponent<Rigidbody2D>().velocity = dirM * bullet1MoveSpeed;
                    bullet.GetComponent<BulletBase>().attack = skill1Damage;
                    bullet.GetComponent<BulletBase>().triggerTag = "Player";
                }
                isMove = false;
                noMoveInterval = skill1NoMoveTime;
            }
           
            yield return new WaitForSeconds(0.5f);
            {//第二波
                {
                    GameObject bullet = Instantiate(bossBullet1Prefabs);
                    Vector2 dirM = new Vector2(Mathf.Cos((curAngle - angleOffect / 2) / 180.0f * Mathf.PI), Mathf.Sin((curAngle - angleOffect / 2) / 180.0f * Mathf.PI)).normalized;
                    bullet.transform.position = transform.position + new Vector3(dirM.x, dirM.y, 0) * bullet1StartDistance;
                    bullet.transform.right = dirM;
                    bullet.GetComponent<Rigidbody2D>().velocity = dirM * bullet1MoveSpeed;
                    bullet.GetComponent<BulletBase>().attack = skill1Damage;
                    bullet.GetComponent<BulletBase>().triggerTag = "Player";
                }
                {
                    GameObject bullet = Instantiate(bossBullet1Prefabs);
                    Vector2 dirM = new Vector2(Mathf.Cos((curAngle + angleOffect / 2) / 180.0f * Mathf.PI), Mathf.Sin((curAngle + angleOffect / 2) / 180.0f * Mathf.PI)).normalized;
                    bullet.transform.position = transform.position + new Vector3(dirM.x, dirM.y, 0) * bullet1StartDistance;
                    bullet.transform.right = dirM;
                    bullet.GetComponent<Rigidbody2D>().velocity = dirM * bullet1MoveSpeed;
                    bullet.GetComponent<BulletBase>().attack = skill1Damage;
                    bullet.GetComponent<BulletBase>().triggerTag = "Player";
                }
                isMove = false;
                noMoveInterval = skill1NoMoveTime;
            }
            Debug.Log(curAngle);
        }

    }
   // public float skill2NoMoveStartTime = 1.5f;
    public float skill2NoMoveEndTime = 0.5f;
    public GameObject bossBullet2Prefabs;
    public int skill2Damage = 20;
    public void Skill2Use()
    {
        //计算角度
        Vector2 dirT = target.position - transform.position;
        float curAngle = 0;
        if (dirT.x == 0)
        {
            if (dirT.y > 0)
            {
                curAngle = 90;
            }
            else
            {
                curAngle = -90;
            }
        }
        else
        {
            curAngle = Mathf.Atan(dirT.y / dirT.x) / Mathf.PI * 180;
            if (dirT.x < 0)
            {
                curAngle += 180;
            }
        }
        Debug.Log("释放技能2");
        GameObject bullet = Instantiate(bossBullet2Prefabs);
        Vector2 dirM = new Vector2(Mathf.Cos((curAngle ) / 180.0f * Mathf.PI), Mathf.Sin((curAngle) / 180.0f * Mathf.PI)).normalized;
        bullet.transform.position = transform.position ;
        bullet.transform.right = dirM;
        bullet.GetComponent<Rigidbody2D>().velocity = dirM * bullet1MoveSpeed;
        bullet.GetComponent<BulletBase>().attack = skill2Damage;
        bullet.GetComponent<BulletBase>().triggerTag = "Player";
        isMove = false;
        noMoveInterval = skill2NoMoveEndTime;
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

}
