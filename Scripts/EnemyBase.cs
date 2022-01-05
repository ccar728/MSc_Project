using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthSC))]
public class EnemyBase : MonoBehaviour
{
    enum MoveType
    {
        Random,
        Target,
    }
    public ElementType elementType;
    Rigidbody2D rig;
   protected Transform target;
    Transform sp;
    protected HealthSC health;
    protected virtual void Awake()
    {
        health = GetComponent<HealthSC>();
        rig = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerSC>().transform;
        sp = transform.Find("EnemySP");
    }
    MoveType moveType = MoveType.Random;
    Vector3 startScale;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        startScale = sp.localScale;
        health.dieEvent += Die;
        health.takeDamageStartEvent += GetHurtStart;
        health.takeDamageEndEvent += GetHurtEnd;
        StartAdd();
    }
    protected virtual void StartAdd()
    {
        Debug.Log("开启移动");
        StartCoroutine(moveIE());
    }
    public float dieEndTime = 0.5f;
    protected virtual void Die(DamageMassage dm)
    {
        StartCoroutine(Die());
    }
    IEnumerator Die()
    {

        yield return new WaitForSeconds(dieEndTime);
        Destroy(this.gameObject);
    }
    public Slider healthUI;
    public Transform hpTextParent;
   // public GameObject textParent;
    void GetHurtEnd(DamageMassage dm)
    {
        healthUI.value = health.curHP / (float)health.maxHP;
        GameObject go = Instantiate(GameController.Instance.textPre);
        go.transform.parent = hpTextParent;
        go.transform.position = hpTextParent.position;
        go.GetComponent<Text>().text = "-" + dm.damage;
        //GameController.Instance.CreateHPText(hpTextParent.position, "-" + dm.damage);
    }
    void GetHurtStart(DamageMassage dm)
    {

    }
    Vector3 moveDir;
    IEnumerator moveIE()
    {
        while (true)
        {
            {
                float moveAngle = Random.Range(0, 360.0f);
                moveDir = new Vector3(Mathf.Sin(moveAngle / 180 * Mathf.PI), Mathf.Cos(moveAngle / 180 * Mathf.PI), 0);
            }
            {
                yield return new WaitForSeconds(2.0f);
                moveType = MoveType.Target;
                float moveAngle = Random.Range(0, 360.0f);
                moveDir = new Vector3(Mathf.Sin(moveAngle / 180 * Mathf.PI), Mathf.Cos(moveAngle / 180 * Mathf.PI), 0);
            }
            {
                yield return new WaitForSeconds(1.5f);
                moveType = MoveType.Random;
            }
            
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        MoveUpdate();
        AttackUpdate();
    }
    public  float moveSpeed = 1.5f;
    protected virtual void MoveUpdate()
    {
        switch (moveType)
        {
            case MoveType.Random:
                {
                   // Debug.Log("随机移动");
                    rig.velocity =moveDir* moveSpeed;
                }
                break;
            case MoveType.Target:
                {
                   // Debug.Log("朝目标");
                    if (target!=null)
                    {
                        rig.velocity = (target.position - transform.position).normalized * moveSpeed;
                    }
                    
                }
                break;
            default:
                break;
        }
      //  Debug.Log(rig.velocity);
        if (rig.velocity.x > 0)
        {
            sp.localScale = new Vector3(startScale.x,startScale.y,startScale.z);
        }
        else if (rig.velocity.x < 0)
        {
            sp.localScale = new Vector3(-startScale.x, startScale.y, startScale.z);
        }
    }
    public Transform firePoint;
    public GameObject createBullet;
    public int attack;
    public float fireBulletSpeed=5;
    public float fireInterval = 0;
    float fireTimer = 0;
    public float attackRange=5;
    protected virtual void AttackUpdate()
    {
        fireTimer += Time.deltaTime;

        if (Vector3.Distance(target.position,transform.position)<attackRange)
        {
            if (fireTimer>fireInterval)
            {
                Attack();
                fireTimer = 0;
            }
        }
    }
    protected virtual void Attack()
    {
        GameObject bullet = Instantiate(createBullet);
        bullet.transform.position = firePoint.position;
        bullet.transform.right=target.position- firePoint.position;
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * fireBulletSpeed;
        bullet.GetComponent<BulletBase>().attack = attack;
        bullet.GetComponent<BulletBase>().triggerTag = "Player";

    }
}
