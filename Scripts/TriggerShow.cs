using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject showGO;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag=="Player")
        {
            showGO.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
