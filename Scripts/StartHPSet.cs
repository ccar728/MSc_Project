using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartHPSet : MonoBehaviour
{
    public float percent=0.25f;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    bool isOneFrame = true;
    // Update is called once per frame
    void Update()
    {
        if (isOneFrame)
        {
            GetComponent<HealthSC>().curHP = (int)(GetComponent<HealthSC>().maxHP * percent);
            GetComponentInChildren<Slider>().value = percent;
            isOneFrame = false;
        }
    }
}
