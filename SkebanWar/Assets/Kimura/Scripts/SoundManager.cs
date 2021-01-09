using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//honnbann
public class SoundManager : MonoBehaviour
{
    static public SoundManager Instans;

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

    //フェードイン初期値
    //フェード再生するか？
    public bool IsFade;
    //フェードインする時間
    float fadeIntime = 0;
    //フェードアウトする時間
    float fadeOuttime = 0;
    //フェードイン・アウトの経過時間
    float fadeTimeLate = 0;
    //フェードイン・アウトの経過時間
    float fadeTimeLate2 = 0;


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

        //AudioClipの読み込み
        for (int i = 0; i < BGMaudioClip.Length; i++)
        {
            bgmIndex.Add(BGMaudioClip[i].name, i);
        }
        for (int i = 0; i < SEaudioClip.Length; i++)
        {
            seIndex.Add(SEaudioClip[i].name, i++);
        }

    }


    void Update()
    {
        //フェードイン(例)
        //if (fadetime > 0)
        //{
        //    fadetime -= Time.deltaTime;
        //    BGM_audioSource.volume = 1 - (fadetime / fadeTimeLate);
        //}
        if (fadeIntime > 0)
        {
            fadeIntime -= Time.deltaTime;
            BGM_audioSource.volume = 1 - (fadeIntime / fadeTimeLate);
        }

        if (fadeOuttime > 0)
        {
            fadeOuttime -= Time.deltaTime;
            BGM_audioSource.volume = (fadeOuttime / fadeTimeLate2);
            if (BGM_audioSource.volume < 0)
            {
                BGM_audioSource.Stop();
            }
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

    //BGM再生
    public void PlayBGM(int index, float fadeIn_Time = 0)
    {
        index = Mathf.Clamp(index, 0, BGMaudioClip.Length);

        //フェードイン時間の設定
        fadeIntime = fadeIn_Time;
        fadeTimeLate = fadeIntime;

        BGM_audioSource.clip = BGMaudioClip[index];
        BGM_audioSource.loop = true;
        BGM_audioSource.volume = BGM_Volume * _Volume;
        BGM_audioSource.Play();
    }

    public void Stop_BGM(int index, float fadeOut_Time = 0)
    {
        //フェードアウト時間の設定
        fadeOuttime = fadeOut_Time;
        fadeTimeLate2 = fadeOuttime;

        BGM_audioSource.clip = BGMaudioClip[index];
        BGM_audioSource.volume = BGM_Volume * _Volume;
    }

    public void PlayBgmName(string name, float fadeintime = 0)
    {
        PlayBGM(GetBgmIndex(name), fadeintime);
    }

    public void StopBgmName(string name, float fadeOuttime = 0)
    {
        Stop_BGM(GetBgmIndex(name), fadeOuttime);
    }
    public void StopBgm()
    {
        BGM_audioSource.Stop();
        BGM_audioSource.clip = null;
    }

    //SE再生
    public void PlaySE(int index)
    {
        SE_audioSource.PlayOneShot(SEaudioClip[index], SE_Volume * _Volume);
    }

    public void PlaySeName(string name)
    {
        PlaySE(GetSeIndex(name));
    }

}
