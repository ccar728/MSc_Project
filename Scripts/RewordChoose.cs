using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewordChoose : MonoBehaviour
{
    Button btn;
    private void Awake()
    {
        btn = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isChoose)
        {
            ChooseThis();
        }
        btn.onClick.AddListener(ChooseThis);
    }
   [SerializeField] 
    public bool isChoose = false;
    [SerializeField]
     Text chooseText;
    void NoChoose()
    {
        chooseText.color = Color.white;
        isChoose = false;
    }
    public int weaponID = 0;
    void ChooseThis()
    {
        RewordChoose[] choose = transform.parent.GetComponentsInChildren<RewordChoose>();
        for (int i = 0; i < choose.Length; i++)
        {
            if (choose[i] == this)
            {
                choose[i].Choose();
            }
            else
            {
                choose[i].NoChoose();
            }
        }
    }
    void Choose()
    {
        chooseText.color = Color.blue;
        isChoose = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
