using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Koinmanager : MonoBehaviour
{
    public Image sampleImage;
    
    //イメージ画像の配列
    public Sprite Koin1;
    public Sprite Koin2;

    //テキストの配列
    public Text _text;

    //ゲームオブジェクトの配列
    public GameObject SousaText;
    public GameObject NextText;

    //クリックチェック
    bool clickcheck = true;

    //回転速度
    float rotSpeed = 0f;

    public Progress progress;

    // Update is called once per frame
    void Update()
    {
        if (clickcheck == true)
        {

            if (Input.GetMouseButtonDown(0))
            {
                this.rotSpeed = 20f;

                //this.rotSpeed -= Time.deltaTime;

                //1秒後にKoin_の実行する
                Invoke("Koin_", 1.0f);

                clickcheck = false;
                SousaText.SetActive(false);

                GameObject obj = GameObject.Find("TouchText");
                Destroy(obj);
            }
        }

        this. transform.Rotate(0, this.rotSpeed, 0);
    }
    void Koin_()
    {
        //先攻後攻を決める処理
        int a = Random.Range(0, 2);
        if (a == 0)
        {
            sampleImage.sprite = Koin1;
            _text.text = "<size=90>1Pが<size=110>先攻</size>です</size>";
            GameManager.order = true;
        }
        else
        {
            sampleImage.sprite = Koin2;
            _text.text = "<size=90>1Pが<size=110>後攻</size>です</size>";
            GameManager.order = false;
        }

        this.rotSpeed = 0;
        transform.eulerAngles = new Vector2(0, 0);
        SousaText.SetActive(true);
        NextText.SetActive(true);
        StartCoroutine(ToMain(1.5f));

    }

    private IEnumerator ToMain(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        //最終的にステージセレクトをする。
        //SceneManager.LoadScene("StageSelect");
        SceneManager.LoadScene("Action");
        yield break;
    }

}
