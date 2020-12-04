using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaundoManager : MonoBehaviour
{
    [SerializeField, Range(0, 1), Tooltip("全体音量")]
    float volume = 1;
    [SerializeField, Range(0, 1), Tooltip("BGM音量")]
    float BGMvolume = 1;
    [SerializeField, Range(0, 1), Tooltip("SE音量")]
    float SEvolume = 1;

    static public SaundoManager Instans;

    public AudioClip[] BGMaudioClip;
    public AudioClip[] SEaudioClip;

    public AudioSource BGM_audioSource;
    public AudioSource SE_audioSource;

    #region soundvolume関係
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

    }

    public void PlayBGM(int num)
    {
        BGM_audioSource.PlayOneShot(BGMaudioClip[num], BGM_Volume * _Volume);
        //BGM_audioSource.volume = BGM_Volume * _Volume;
    }

    public void PlaySE(int num)
    {
        SE_audioSource.PlayOneShot(SEaudioClip[num], SE_Volume * _Volume);
    }

}
