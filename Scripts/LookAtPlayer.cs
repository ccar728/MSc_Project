using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    Rigidbody2D rig;
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    public Transform targetPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float moveSpeed = 1.5f;
    public float addSpeed = 1.5f;
    // Update is called once per frame
    void Update()
    {
      
    }
    private void LateUpdate()
    {
        if (targetPlayer == null)
        {
            return;
        }
        Vector3 targetPos = targetPlayer.position;
        targetPos.z = transform.position.z;
        Vector3 thisPos = transform.position;
        float distance =Vector3.Distance(thisPos,targetPos);
        float speed = moveSpeed + distance * addSpeed;
        float moveDistance =speed*Time.deltaTime;
        if (moveDistance > distance)
        {
            rig.MovePosition(targetPos);
            //rig.velocity = Vector2.zero;
        }
        else
        {
            rig.MovePosition(rig.position+(Vector2)((targetPos-thisPos).normalized)*moveDistance);
        }
       
    }
}
