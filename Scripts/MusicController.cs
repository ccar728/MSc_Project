using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    static List<AudioSource> effectList = new List<AudioSource>();

    //音乐单例
    private static MusicController instance = null;
    public static MusicController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                go.transform.position = Vector3.zero;
                myAudio = go.AddComponent<AudioSource>();
                instance = go.AddComponent<MusicController>();
                go.name = "Music(Auto*Create)";
                myAudio.loop = true;


            }
            return instance;
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            myAudio = GetComponent<AudioSource>();
        }
        else
        {
            if (instance.gameObject != this.gameObject)
            {
                Destroy(this.gameObject);
            }
        }

    }
    static AudioSource myAudio;
    //添加手动修改
    private void OnValidate()
    {
        if (myAudio != null)
        {
            myAudio.volume = isOpenEF ? bgVolume : 0;
        }
        else
        {
            myAudio = GetComponent<AudioSource>();
            myAudio.volume = isOpenEF ? bgVolume : 0;
        }
    }

    /// <summary>
    /// 设置背景音乐
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="isReset"></param>
    public void SetBGClip(AudioClip clip, bool isReset = false)
    {
        if (isReset)
        {
            myAudio.clip = clip;
            myAudio.Play();
        }
        else
        {
            if (clip != myAudio.clip)
            {
                myAudio.clip = clip;
                myAudio.Play();
            }
        }
    }
    //背景音乐音量
    [SerializeField]
    float bgVolume = 1.0f;
    public float BgVolume
    {
        get
        {
            return bgVolume;
        }
        set
        {
            bgVolume = value;
            myAudio.volume = bgVolume;
        }
    }
    public bool isOpenBG = true;
    //音效音量
    public float effectVolume = 1.0f;
    public bool isOpenEF = true;
    // Start is called before the first frame update
    void Start()
    {
        //初始化
        myAudio.volume = isOpenEF ? bgVolume : 0;
        DontDestroyOnLoad(this.gameObject);

    }

    public List<AudioClip> efClipList = new List<AudioClip>();
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="音效"></param>
    /// <param name="volume"></param>
    public void PlayEffectByFrame(AudioClip clip, bool isJumpScene = true,float? volume = null)
    {
        if (volume == null)
        {
            volume = effectVolume;
        }
        if (!efClipList.Contains(clip))
        {
            efClipList.Add(clip);
            //
            if (isJumpScene)
            {
                bool isHas = false;
                for (int i = 0; i < effectList.Count; i++)
                {
                    if (!effectList[i].isPlaying)
                    {
                        effectList[i].PlayOneShot(clip, (float)volume);
                        isHas = true;
                        break;
                    }
                }
                if (!isHas)
                {
                    GameObject effect = new GameObject();
                    effect.transform.parent = transform;
                    effect.AddComponent<AudioSource>();
                    effect.transform.localPosition = Vector3.zero;
                    effectList.Add(effect.GetComponent<AudioSource>());
                    //Debug.Log("播放"+clip);
                    //effect.GetComponent<AudioSource>().PlayOneShot(clip, (float)volume);
                    effect.GetComponent<AudioSource>().clip = clip;
                    effect.GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                AudioSource.PlayClipAtPoint(clip, transform.position, isOpenEF ? (float)volume : 0);
            }
           
        }

    }
    /// <summary>
    /// 播放自带间隔的音乐
    /// </summary>
    /// <param name="audioMassage"></param>
    /// <param name="volume"></param>
    public void PlayEffectByMassage(AudioMassage audioMassage,bool isJumpScene=false, float? volume = null)
    {
        if (volume == null)
        {
            volume = effectVolume;
        }

        StartCoroutine(EffectMassageIE(audioMassage,isJumpScene, volume));
    }
    List<AudioClip> audioMassageList = new List<AudioClip>();
    IEnumerator EffectMassageIE(AudioMassage audioMassage,bool isjumpScene, float? volume)
    {
        if (audioMassageList.Contains(audioMassage.clip))
        {
            yield break;
        }
        audioMassageList.Add(audioMassage.clip);
        if (isjumpScene)
        {
            bool isHas = false;
            for (int i = 0; i < effectList.Count; i++)
            {
                if (!effectList[i].isPlaying)
                {
                    effectList[i].PlayOneShot(audioMassage.clip, (float)volume);
                    isHas = true;
                    break;
                }
            }
            if (!isHas)
            {
                GameObject effect = new GameObject();
                effect.transform.parent = transform;
                effect.AddComponent<AudioSource>();
                effect.transform.localPosition = Vector3.zero;
                effectList.Add(effect.GetComponent<AudioSource>());
                effect.GetComponent<AudioSource>().PlayOneShot(audioMassage.clip, (float)volume);
            }

        }
        else
        {
            AudioSource.PlayClipAtPoint(audioMassage.clip, transform.position, isOpenEF ? (float)volume : 0);
        }
       

        yield return new WaitForSeconds(audioMassage.time);
        audioMassageList.Remove(audioMassage.clip);

    }
    //清除音效链表
    private void LateUpdate()
    {
        efClipList.Clear();
    }
   
}
public class AudioMassage
{
    public AudioClip clip;
    public float time = 0;
    public AudioMassage(AudioClip clip, float time)
    {
        this.clip = clip;
        this.time = time;
    }

}
