 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    SpriteRenderer sr;
    BoxCollider2D box;
    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    public Sprite openSP;
    public bool isOpen = false;
    public void Open()
    {
        sr.sprite = openSP;
        //box.isTrigger = true;
        isOpen = true;
    }
    public string sceneName="";
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag=="Player"&isOpen)
        {
            Debug.Log("游戏通关");
            SceneManager.LoadScene(sceneName);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
