using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemyProp : MonoBehaviour
{
    [SerializeField]
    Sprite closeSP;
    bool isOpen = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpen)
        {
            return;
        }
        if (collision.GetComponent<PlayerSC>())
        {
            isOpen = true;
            GameController.Instance.StartEenemy();
            GetComponent<SpriteRenderer>().sprite=closeSP;
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
