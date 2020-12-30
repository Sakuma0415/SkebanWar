﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManger : MonoBehaviour
{
    [SerializeField]
    Fade fade = null;
    static public SceneManger Instans;

    public void Scene(int num)
    {
        if (Instans == null)
        {
            Instans = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            fade.FadeIn(1, () =>
            {
                fade.FadeOut(1, () =>
                {
                    SceneManager.LoadScene("SampleScene2");
                });
            });
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            fade.FadeIn(1, () =>
            {
                fade.FadeOut(1, () =>
                {
                    SceneManager.LoadScene("SampleScene");
                });
            });
        }
    }
}
