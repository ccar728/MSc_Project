using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackMenuButton : MonoBehaviour
{
    Button btn;
    private void Awake()
    {
        btn = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(() => 
        {

            SceneManager.LoadScene("MenuScene");

        });
    }
}
