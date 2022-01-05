using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    Button btn;
    private void Awake()
    {
        if (GetComponent<Button>())
        {
            btn = GetComponent<Button>();
        }
        else
        {
            btn = gameObject.AddComponent<Button>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(Application.Quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
