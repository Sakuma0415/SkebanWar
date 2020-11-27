using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class test : MonoBehaviour
{
    //private float step_time;
    // Start is called before the first frame update
    void Start()
    {
        //タイマー初期化
        //step_time = 0.0f;

        //BGM、SEが流れる
        SaundoManager.Instans.PlaySE(0);

    }

    // Update is called once per frame
    void Update()
    {
        //タイマーが加算されていく
        //step_time += Time.deltaTime;
        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    SaundoManager.Instans.PlaySE(0);
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    SceneManager.LoadScene("test2");
        //}
        //２秒後に画面遷移される
        //if (step_time >= 2.0f)
        //{
        //    SceneManager.LoadScene("test2");
        //    //Debug.Log("切り替えた");
        //}

    }
}
