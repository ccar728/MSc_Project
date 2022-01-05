using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public int attack;
    public ElementType elementType;
    public float fireInterval;
    public float fireBulletSpeed;
    public Transform firePoint;
    public GameObject bulletPrefabs;
    bool isCanFire = true;
    bool isReloading = false;
    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }
    public float reloadUseTime = 0.2f;
    float reloadTimer = 0;
    protected float fireTimer = 0;
    public abstract void Fire();
    public int maxBullet = 2;//最大装弹
    public int curBullet = 2;//当前装弹
    public bool isActive = false;
    //public abstract void ReloadBullet();
    public virtual void ReloadBulletJudge()
    {
        if (!isReloading)
        {
            isReloading = true;
        }
    }

    protected virtual void Update()
    {
        //发射计时
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            fireTimer = fireInterval;
        }
        //加载子弹计时
        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer>=reloadUseTime)
            {
                ReloadBullet();
                isReloading = false;
                reloadTimer = 0;
            }
        }
    }
    public System.Action reloadingEnd;
    protected virtual void ReloadBullet()
    {
        curBullet = maxBullet;
        reloadingEnd?.Invoke();
    }
    public System.Action fireEnd;
    public void FireJudge()
    {
        if (isReloading||curBullet<=0)
        {
            return;
        }
        if (fireTimer >= fireInterval)
        {
            curBullet--;
            Fire();
            fireEnd?.Invoke();
            fireTimer %= fireInterval;
        }
    }

}
