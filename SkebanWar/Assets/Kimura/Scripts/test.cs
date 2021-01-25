using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{

    void Start()
    {
        //フェードインしてBGMが流れる
        SoundManager.Instans.FadeInBGM("Ataku");

    }

    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    SoundManager.Instans.FadeInBGM("TestBGM");
        //}
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    //BGMがフェードアウトする
        //    SoundManager.Instans.FadeOutBGM();

        //}


        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("SenkouKime");
            //SoundManager.Instans.PlaySeName("TestSE");
        }

    }
    



}
