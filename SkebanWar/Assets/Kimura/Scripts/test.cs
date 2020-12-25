using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            //SaundoManager.Instans.PlaySeName("TestSE");
            SoundManager.Instans.PlayBgmName("TestBGM",7f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //SaundoManager.Instans.PlaySeName("TestSE");
            SoundManager.Instans.PlaySeName("TestSE");
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("test2");
        }

    }
    



}
