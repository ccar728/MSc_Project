using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHPProp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (LocalData.playerAddHpProp!=null)
        {
            count = (int)LocalData.playerAddHpProp;
            text.text = "" + count;
        }
    }
    private void OnDestroy()
    {
        LocalData.playerAddHpProp = count;
    }
    int count = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Use();
        }
    }
    void Use()
    {
        if (count>0)
        {
            HealthSC health = GameObject.FindObjectOfType<PlayerSC>().GetComponent<HealthSC>();
           health.AddHP((int)((float)health.maxHP/3));
            count--;
            text.text = "" + count;
        }
    }
    [SerializeField]
    Text text;
    public void AddCount(int count=1)
    {
        this.count += count;
        text.text = "" + this.count;
    }
}
