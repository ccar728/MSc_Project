using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    public int attack = 3;
    public string triggerTargetTag;
    public float deleyTime = 1.2f;
    public float destoryTime = 1.5f;
    public ElementType elementType;
    private void Start()
    {
        Destroy(this.gameObject,destoryTime);
        StartCoroutine(attackIE());
    }
    IEnumerator attackIE()
    {
        yield return new WaitForSeconds(deleyTime);
        //清空没必要的
        for (int i = 0; i < healthtarget.Count; i++)
        {
            if (healthtarget[i]!=null)
            {
                healthtarget[i].TakeDamage(new DamageMassage( attack, elementType));
            }
        }
    }
    List<HealthSC> healthtarget = new List<HealthSC>();
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag== triggerTargetTag&&col.GetComponent<HealthSC>())
        {
            healthtarget.Add(col.GetComponent<HealthSC>());
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<HealthSC>())
        {
            if (healthtarget.Contains(col.GetComponent<HealthSC>()))
            {
                healthtarget.Remove(col.GetComponent<HealthSC>());
            }
        }
    }
}
