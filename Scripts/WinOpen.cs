using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOpen : MonoBehaviour
{
    [SerializeField]
    Door door;
    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.winEvent += ()=> 
        {
            door.Open();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
