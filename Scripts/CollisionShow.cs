using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionShow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool isOpen = false;
    public Sprite OpenSP;
    public void SetOpen()
    {
        isOpen = true;
        GetComponent<SpriteRenderer>().sprite=OpenSP;
    }
    public GameObject showGO;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag=="Player"&&isOpen)
        {
            showGO.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
