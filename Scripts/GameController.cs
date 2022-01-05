using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public int level = 1;

    public static GameController Instance = null;
    Camera uiCamera;
    private void Awake()
    {
        Instance = this;
        Debug.Log("管理器初始化成功");
        uiCamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        PlayerPrefs.SetInt("Level",level);
    }
    
  
    public Slider playerHPSlider;
    public void UpdateHPSlider(float value)
    {
        playerHPSlider.value = value;
    }
    public bool isStartShow = true;
    public void StartEenemy()
    {
        if (enemyWave.Length > 0)
        {
            for (int i = 0; i < enemyWave.Length; i++)
            {
                enemyWave[i].gameObject.SetActive(false);
            }
            enemyWave[waveIndex].gameObject.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isStartShow)
        {
            StartCoroutine(StartEnemeyWaveIE());
        }
        
    }
    int stopLayer = 0;
    public void SetStop()
    {
        stopLayer++;
        if (stopLayer == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    public void SetContinue()
    {
        stopLayer--;
        if (stopLayer == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    

    bool isOver = false;
    public void GameLose()
    {
        if (isOver)
        {
            return;
        }
        isOver = true;
        StartCoroutine(LoseIE());
    }
    [SerializeField]
    GameObject loseGo;
    IEnumerator LoseIE()
    {
        yield return new WaitForSeconds(0.5f);
        loseGo.SetActive(true);
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }





    public Transform[] enemyWave;
    public int waveIndex = 0;
    public float startTime = 10;
    IEnumerator StartEnemeyWaveIE()
    {
       
        if (enemyWave.Length>0)
        {
            for (int i = 0; i < enemyWave.Length; i++)
            {
                enemyWave[i].gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(startTime);
            enemyWave[waveIndex].gameObject.SetActive(true);
        }
    }
    bool isWin = false;
    // Update is called once per frame
    void Update()
    {
        if (isWin)
        {
            return;
        }
        if (enemyWave.Length > 0)
        {
            if (enemyWave[waveIndex].childCount==0)
            {
                Destroy(enemyWave[waveIndex].gameObject);
                waveIndex++;
                if (enemyWave.Length <=waveIndex)
                {
                    winEvent?.Invoke();
                    isWin = true;
                }
                else
                {
                    enemyWave[waveIndex].gameObject.SetActive(true);

                }
            }
        }
    }
    public delegate void WinDelegate();
    public event WinDelegate winEvent;

    public GameObject textPre;
    public Transform createTextParent;
    public void CreateHPText(Vector3 createWorldPos,string text)
    {
        GameObject textT = Instantiate(textPre);
        textT.transform.SetParent(  createTextParent,false);
        Vector3 wPos = Camera.main.WorldToScreenPoint(createWorldPos);
        Vector3 pos = uiCamera.ScreenToWorldPoint(wPos);
        textT.transform.position = pos;
        Vector3 local=textT.transform.localPosition;
        local.z = 0;
        textT.transform.localPosition = local;
        textT.GetComponent<Text>().text=text;
    }
    public Text bulletText;
    public void SetBulletText(int curBullet,int maxBullet)
    {
        bulletText.text = curBullet + "/" + maxBullet;
    }
}
