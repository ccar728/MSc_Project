using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBlackProp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }
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
            StartCoroutine(Open());
            
        }
    }
    [SerializeField]
    GameObject black;
    IEnumerator Open()
    {
        isOpen = true;
        GetComponent<SpriteRenderer>().sprite = closeSP;
        black.SetActive(true);
        yield return new WaitForSeconds(5f);
        black.SetActive(false);
        GameController.Instance.StartEenemy();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
