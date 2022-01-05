using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateThis : MonoBehaviour
{
    public float rotateSpeed =1800;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward*rotateSpeed*Time.deltaTime);
    }
}
