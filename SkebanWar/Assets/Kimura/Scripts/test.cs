﻿using System.Collections;
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

        if (Input.GetKeyDown(KeyCode.I))
        {
            SoundManager.Instans.FadeInBGM("TestBGM");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SoundManager.Instans.FadeOutBGM();

        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("test2");
            SoundManager.Instans.FadeInBGM("Ataku");
        }

    }
    



}
