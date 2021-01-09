using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    static public SoundManager Instans;

    private enum SoundFadeMode
    {
        Idle,
        Fadein,
        FadeOut
    }

    //ボリューム関係
    [SerializeField, Range(0, 1), Tooltip("全体音量")]
    float volume = 1;
    [SerializeField, Range(0, 1), Tooltip("BGM音量")]
    float BGMvolume = 1;
    [SerializeField, Range(0, 1), Tooltip("SE音量")]
    float SEvolume = 1;

    [SerializeField] private float _FadeInTime = 3;
    [SerializeField] private float _FadeOutTime = 5;

    [SerializeField] private List<AudioClip> BGMaudioClip = new List<AudioClip>();
    public AudioClip[] SEaudioClip;

    public AudioSource BGM_audioSource;
    public AudioSource SE_audioSource;

    Dictionary<string, int> bgmIndex = new Dictionary<string, int>();
    Dictionary<string, int> seIndex = new Dictionary<string, int>();

    private SoundFadeMode _soundFadeMode = SoundFadeMode.Idle;

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
        for (int i = 0; i < SEaudioClip.Length; i++)
        {
            seIndex.Add(SEaudioClip[i].name, i++);
        }

    }

    private IEnumerator SoundFadeIn()
    {
        while (BGM_audioSource.volume < 1)
        {
            BGM_audioSource.volume += (1.0f / _FadeInTime) * Time.deltaTime;
            if (BGM_audioSource.volume >= 1)
                BGM_audioSource.volume = 1;
            yield return null;
        }
        _soundFadeMode = SoundFadeMode.Idle;
    }

    private IEnumerator SoundFadeOut()
    {
        while (BGM_audioSource.volume > 0)
        {
            BGM_audioSource.volume -= (1.0f / _FadeOutTime) * Time.deltaTime;
            if (BGM_audioSource.volume <= 0)
                BGM_audioSource.volume = 0;
            yield return null;
        }
        _soundFadeMode = SoundFadeMode.Idle;
    }

    public int GetBgmIndex(string name)
    {
        if (bgmIndex.ContainsKey(name))
        {
            return bgmIndex[name];
        }
        else
        {
            Debug.LogError("BGMファイルが存在しません。");
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
            Debug.LogError("SEファイルが存在しません。");
            return 0;
        }
    }


    //BGM再生
    public void FadeInBGM(string fileName)
    {
        if (SoundFadeMode.Idle == _soundFadeMode)
        {
            _soundFadeMode = SoundFadeMode.Fadein;
            var index = BGMaudioClip.FindIndex(x => x.name.Equals(fileName));
            if (-1 == index)
                return;
            BGM_audioSource.clip = BGMaudioClip[index];
            BGM_audioSource.volume = 0;
            BGM_audioSource.Play();
            StartCoroutine(SoundFadeIn());
        }
    }

    public void FadeOutBGM()
    {
        if (SoundFadeMode.Idle == _soundFadeMode)
        {
            _soundFadeMode = SoundFadeMode.FadeOut;
            StartCoroutine(SoundFadeOut());
        }
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
