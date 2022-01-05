using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : WeaponBase
{
    public override void Fire()
    {
        MusicController.Instance.PlayEffectByFrame(Resources.Load<AudioClip>("Music/PlayerFire"));
        GameObject bullet = Instantiate(bulletPrefabs);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.GetComponent<Rigidbody2D>().velocity=bullet.transform.right*fireBulletSpeed;
        bullet.GetComponent<BulletBase>().attack=attack;
        bullet.GetComponent<BulletBase>().triggerTag = "Enemy";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
