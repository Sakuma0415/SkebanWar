using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaundoManager : MonoBehaviour
{
    static public SaundoManager Instans;
    public AudioClip[] audioClip;
    public AudioSource audioSource;
    private float step_time;

    //音楽流す
    public void PlaySE(int num)
    {
        audioSource.PlayOneShot(audioClip[num]);
    }

    // Start is called before the first frame update
    void Start()
    {
        //タイマー初期化
        step_time = 0.0f;

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

    // Update is called once per frame
    void Update()
    {
        step_time += Time.deltaTime;

        if (step_time >= 2.0f)
        {
            SceneManager.LoadScene("test2");
            //Debug.Log("切り替えた");
        }


    }
}
