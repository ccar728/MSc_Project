using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(moveIE());
    }
    IEnumerator moveIE()
    {
        yield return new WaitForSeconds(0.7f);
        isMove = true;
    }
    bool isMove = false;
    public float height = 7;
    Vector3 moveTarget;
    public GameObject moveEndSHowPre;
    public GameObject moveStartShowPre;
    GameObject moveStartShow;
    
    public void Init(Vector3 pos)
    {
        transform.position = pos + new Vector3(0, height,0);
        moveTarget = pos;
        moveStartShow = Instantiate(moveStartShowPre);
        moveStartShow.transform.position = pos;
    }
    public float downSpeed = 10;
    public string targetStr;
    public int attack;
    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            float distance = Vector3.Distance(moveTarget,transform.position);
            float moveDistance = downSpeed * Time.deltaTime;
            if (moveDistance < distance)
            {
                transform.Translate((moveTarget-transform.position).normalized*moveDistance);
            }
            else
            {
                transform.position = moveTarget;
                isMove = false;
                GameObject moveEndShow = Instantiate(moveEndSHowPre);
                moveEndShow.transform.position = moveTarget;
                moveEndShow.GetComponent<BoomEffect>().triggerTargetTag = targetStr;
                moveEndShow.GetComponent<BoomEffect>().attack = attack;
                Destroy(moveStartShow);
                Destroy(this.gameObject);
            }
        }
    }
}
