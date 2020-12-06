using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{

    void Start()
    {
        SaundoManager.Instans.PlayBgmName("TestBGM");
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            SaundoManager.Instans.PlaySeName("TestSE");

        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("test2");
        }

    }
    



}
