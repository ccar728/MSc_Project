using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpAndDestory : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();

    }

    public float upSpeed = 3.0f;
    public float rightSpeed = 1f;
    public float destoryTime = 1.5f;
    public float hideSpeed = 0.8f;

    float destoryTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        destoryTimer += Time.deltaTime;
        if (destoryTimer>=destoryTime)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector3.up*upSpeed*Time.deltaTime);
        transform.Translate(Vector3.right * rightSpeed * Time.deltaTime);
        text.color -= new Color(0,0,0,1)*Time.deltaTime*hideSpeed;
    }
}
