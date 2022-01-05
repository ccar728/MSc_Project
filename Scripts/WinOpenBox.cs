using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOpenBox : MonoBehaviour
{
    public CollisionShow box;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.winEvent += ()=> 
        {
            box.SetOpen();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
