using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeve : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool isOver = false;
    System.Action overAction = null;
    // Update is called once per frame
    void Update()
    {
        if (isOver)
        {
            return;
        }
        if (transform.childCount==0)
        {
            isOver = true;
            overAction?.Invoke();
        }   
    }

}
