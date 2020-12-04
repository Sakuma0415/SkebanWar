using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class test : MonoBehaviour
{

    void Start()
    {
        SaundoManager.Instans.PlayBGM(0);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            SaundoManager.Instans.PlaySE(0);

        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("test2");
        }

    }
    



}
