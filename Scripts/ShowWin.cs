using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField]
    GameObject winGO;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerSC>())
        {
            winGO.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
