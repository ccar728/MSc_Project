using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBounding : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<BulletBase>())
        {
            Destroy(col.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
