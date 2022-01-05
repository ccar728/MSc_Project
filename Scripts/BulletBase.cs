using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public int attack = 0;
    public string triggerTag;
    public ElementType type = ElementType.Fire;
    bool istrigger = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (istrigger)
        {
            return;
        }
        if (col.GetComponent<HealthSC>()&&col.tag==triggerTag)
        {
            MusicController.Instance.PlayEffectByFrame(Resources.Load<AudioClip>("Music/BulletTrigger"));
            col.GetComponent<HealthSC>().TakeDamage(new DamageMassage(attack,type));
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(this.gameObject,0.1f);
            istrigger = true;
        }
    }
}
