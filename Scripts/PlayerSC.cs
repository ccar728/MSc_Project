using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ElementType
{
    None,//无
    Fire,//火
    Ice,//冰
    Lighting,//雷
    Wing,//风
}
[RequireComponent(typeof(HealthSC))]
public class PlayerSC : MonoBehaviour
{
    
    public float moveSpeed = 2.0f;
    Transform player;
    Rigidbody2D rig;
    HealthSC health;
    WeaponBase weapon=null;
    Transform weaponParent;
    
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        weaponParent = transform.Find("WeaponParent").transform;
        
        player = transform.Find("Player") ;
        health = GetComponent<HealthSC>();
        
        //初始化玩家血量
        if (LocalData.playercurHP!=null)
        {
            health.curHP = (int)LocalData.playercurHP;
            GameController.Instance.UpdateHPSlider(health.curHP / (float)health.maxHP);
        }
       
    }
    
    Vector3 startScale;
    int curWeaponIndex = 0;
    void ChangeWeapon(int weaponIndex)
    {
        for (int i = 0; i < weaponParent.childCount; i++)
        {
            if (i==weaponIndex)
            {
                weaponParent.GetChild(i).gameObject.SetActive(true);
                weapon = weaponParent.GetChild(i).GetComponent<WeaponBase>();
            }
            else
            {
                weaponParent.GetChild(i).gameObject.SetActive(false);
            }
            
        }
        RefeshBullet();


    }
    public void AddWeapon(int weaponID)
    {
        if (!LocalData.activeWeaopnList.Contains(weaponID))
        {
            LocalData.activeWeaopnList.Add(weaponID);
        }

        weaponParent.GetChild(weaponID).GetComponent<WeaponBase>().isActive = true;
    }
    public GameObject reloadingShow ;
    // Start is called before the first frame update
    void Start()
    {
        ChangeWeapon(0);
        //初始化激活武器
        for (int i = 0; i < LocalData.activeWeaopnList.Count; i++)
        {
            if (weaponParent.childCount > LocalData.activeWeaopnList[i])
            {
                weaponParent.GetChild(LocalData.activeWeaopnList[i]).GetComponent<WeaponBase>().isActive = true;
            }
            else
            {
                weaponParent.GetChild(LocalData.activeWeaopnList[i]).GetComponent<WeaponBase>().isActive = false;
            }
        }
        startScale = player.localScale;
        health.takeDamageEndEvent += (DamageMassage dm) =>
        {
            GameController.Instance.UpdateHPSlider(health.curHP/(float ) health.maxHP);
        };
        health.replayCount += () =>
        {
            GameController.Instance.UpdateHPSlider(health.curHP / (float)health.maxHP);
        };
     health.dieEvent += (DamageMassage dm) =>
        {
            MusicController.Instance.PlayEffectByFrame(Resources.Load<AudioClip>("Music/PlayerDie"));
            GameController.Instance.GameLose();
        };
        //所有武器添加装弹事件
        for (int i = 0; i < weaponParent.childCount; i++)
        {
            weaponParent.GetChild(i).GetComponent<WeaponBase>().reloadingEnd=()=> 
            {
                reloadingShow.SetActive(false);
            };
        }
        reloadingShow.SetActive(false);
    }
    float h;
    float v;
    Vector3 mouseOffect;
    // Update is called once per frame
    void Update()
    {
        {
            if (!weapon.IsReloading)
            {
                //切换武器
                Vector2 mouseSroll = Input.mouseScrollDelta;
                //Debug.Log(mouseSroll);
                float mouseY = mouseSroll.y;
                int moveCount = (int)(mouseY / 1);
                for (int i = 0; i < Mathf.Abs(moveCount); i++)
                {
                    curWeaponIndex += (int)(1 * Mathf.Sign(moveCount));
                    Debug.Log(curWeaponIndex);
                    
                    if (curWeaponIndex < 0)
                    {
                        curWeaponIndex = weaponParent.childCount - 1;
                    }
                    if (curWeaponIndex > weaponParent.childCount - 1)
                    {
                        curWeaponIndex = 0;
                    }
                    if (!weaponParent.GetChild(curWeaponIndex).GetComponent<WeaponBase>().isActive)
                    {
                        i--;
                    }
                }
                ChangeWeapon(curWeaponIndex);
            }
        }
        {
            //装填子弹
            if (Input.GetMouseButtonDown(1))
            {
                reloadingShow.SetActive(true);
                weapon.ReloadBulletJudge();
                RefeshBullet();
            }
        }

        MoveUpdate();
        AttackUpdate();
    }
    void MoveUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        rig.velocity = new Vector2(h, v).normalized * moveSpeed;
        //发射和旋转
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = transform.position.z;
        mouseOffect = pos - transform.position;
        if (mouseOffect.x > 0)
        {
            player.localScale = new Vector3(startScale.x, startScale.y, startScale.z);
        }
        else if (mouseOffect.x < 0)
        {
            player.localScale = new Vector3(-startScale.x, startScale.y, startScale.z);
        }
    }
   
    void AttackUpdate()
    {
        if (health.isDie)
        {
            return;
        }
        weapon.transform.right = mouseOffect;
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }
    
    void Attack()
    {
        weapon.FireJudge();
        RefeshBullet();
    }

    void RefeshBullet()
    {

        GameController.Instance.SetBulletText(weapon.curBullet,weapon.maxBullet);
    }
    private void OnDestroy()
    {

        LocalData.playercurHP=health.curHP;
       
    }
}
