using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    Button btn;
    private void Awake()
    {
        btn = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(()=>{ SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
