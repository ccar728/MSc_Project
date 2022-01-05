using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBullet : BulletBase
{
    public float subTime = 1.5f;
    private void Start()
    {

        StartCoroutine(subIE());
    }
    public int subCount = 6;
    public GameObject subBulletPre;
    public float subBulletSpeed = 2.0f;
    IEnumerator subIE()
    {
        yield return new WaitForSeconds(subTime);

        float oneAngle = 360 / subCount;
        for (int i = 0; i < subCount; i++)
        {
            GameObject bullet = Instantiate(subBulletPre);
            Vector2 dir = new Vector2(Mathf.Cos(oneAngle * i / 180.0f * Mathf.PI), Mathf.Sin(oneAngle * i / 180.0f * Mathf.PI)).normalized;
            bullet.transform.position = transform.position;
            bullet.transform.right = dir;
            bullet.GetComponent<Rigidbody2D>().velocity = dir * subBulletSpeed;
            bullet.GetComponent<BulletBase>().attack = attack;
            bullet.GetComponent<BulletBase>().triggerTag = this.triggerTag;
        }

        gameObject.SetActive(false);
    }
}
