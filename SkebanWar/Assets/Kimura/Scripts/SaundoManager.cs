using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaundoManager : MonoBehaviour
{
    static public SaundoManager Instans;

    //ボリューム関係
    [SerializeField, Range(0, 1), Tooltip("全体音量")]
    float volume = 1;
    [SerializeField, Range(0, 1), Tooltip("BGM音量")]
    float BGMvolume = 1;
    [SerializeField, Range(0, 1), Tooltip("SE音量")]
    float SEvolume = 1;


    public AudioClip[] BGMaudioClip;
    public AudioClip[] SEaudioClip;

    public AudioSource BGM_audioSource;
    public AudioSource SE_audioSource;

    Dictionary<string, int> bgmIndex = new Dictionary<string, int>();
    Dictionary<string, int> seIndex = new Dictionary<string, int>();

    #region soundvolume関係
    //全体ボリューム
    public float _Volume
    {
        set
        {
            volume = Mathf.Clamp01(volume);
            BGM_audioSource.volume = BGMvolume * volume;
            SE_audioSource.volume = SEvolume * volume;
        }
        get
        {
            return volume;
        }
    }
    //BGMボリューム
    public float BGM_Volume
    {
        set
        {
            BGMvolume = Mathf.Clamp01(volume);
            BGM_audioSource.volume = BGMvolume * volume;
        }
        get
        {
            return BGMvolume;
        }
    }

    //SEボリューム
    public float SE_Volume
    {
        set
        {
            SEvolume = Mathf.Clamp01(volume);
            SE_audioSource.volume = SEvolume * volume;
        }
        get
        {
            return SEvolume;
        }
    }

    #endregion
    public void Awake()
    {
        //呼びだされる
        if (Instans == null)
        {
            Instans = this;
            DontDestroyOnLoad(this);
        }
        //ゲームオブジェクトが消される
        else
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < BGMaudioClip.Length; i++)
        {
            bgmIndex.Add(BGMaudioClip[i].name, i);
        }
        for (int i = 0; i < SEaudioClip.Length; i++)
        {
            seIndex.Add(SEaudioClip[i].name, i++);
        }

    }

    public int GetBgmIndex(string name)
    {
        if (bgmIndex.ContainsKey(name))
        {
            return bgmIndex[name];
        }
        else
        {
            return 0;
        }
    }

    public int GetSeIndex(string name)
    {
        if (seIndex.ContainsKey(name))
        {
            return seIndex[name];
        }
        else
        {
            return 0;
        }
    }



    public void PlayBGM(int index)
    {
        index = Mathf.Clamp(index, 0, BGMaudioClip.Length);

        BGM_audioSource.PlayOneShot(BGMaudioClip[index], BGM_Volume * _Volume);
    }

    public void PlayBgmName(string name)
    {
        PlayBGM(GetBgmIndex(name));
    }

    public void PlaySE(int index)
    {
        SE_audioSource.PlayOneShot(SEaudioClip[index], SE_Volume * _Volume);
    }
    public void PlaySeName(string name)
    {
        PlaySE(GetSeIndex(name));
    }

}
