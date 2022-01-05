using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableShow : MonoBehaviour
{
    [SerializeField]
    GameObject showGo;
    private void OnEnable()
    {
        StartCoroutine(showIE());
    }
    public float time = 3.5f;
    IEnumerator showIE()
    {
        showGo.SetActive(true);
        yield return new WaitForSeconds(time);
        showGo.SetActive(false);
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
