using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public void LoadNewGame()
    {
        SceneManager.LoadScene("GameScene" +  1);
    }
    public void LoadLastGame()
    {
        SceneManager.LoadScene("GameScene"+PlayerPrefs.GetInt("Level",1));
    }
    // Start is called before the first frame update
    void Start()
    {
        MusicController.Instance.BgVolume = 0.5f;
        MusicController.Instance.SetBGClip(Resources.Load<AudioClip>("Music/BG"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
