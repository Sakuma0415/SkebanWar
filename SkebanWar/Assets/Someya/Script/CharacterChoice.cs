using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// キャラクター選択画面を管理するクラス
/// </summary>
public class CharacterChoice : MonoBehaviour
{
    // 画面移行する変数
    [SerializeField]
    int select = 0;

    [SerializeField]
    GameObject[] Character;

    // はい、いいえの○を表示する変数
    [SerializeField]
    int choice = 0;

    [SerializeField]
    GameObject[] Decision;

    [SerializeField]
    GameObject text1P;

    [SerializeField]
    GameObject text2P;

    Button buttonA;
    Button buttonI;
    Button buttonN;

    // テキストの枠画像
    [SerializeField]
    GameObject Image1P;

    [SerializeField]
    GameObject Image2P;

    // ボタンを押したら選んだキャラクターを描画
    public void AkaneChange1P()
    {
        choice = 0;
        select = 1;
    }

    public void IoriChange1P()
    {
        choice = 0;
        select = 2;
    }

    public void NanaChange1P()
    {
        choice = 0;
        select = 3;
    }
    public void AkaneChange2P()
    {
        choice = 0;
        select = 5;
    }

    public void IoriChange2P()
    {
        choice = 0;
        select = 6;
    }

    public void NanaChange2P()
    {
        choice = 0;
        select = 7;
    }

    // いいえを押したらキャラクター選択に戻る
    public void AkaneCancel1P()
    {
        choice = 1;
        // 1.5秒後にシーン(画像)に移行
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            select = 0;
        }));
    }
    public void AkaneCancel2P()
    {
        choice = 7;
        // 1.5秒後にシーン(画像)に移行
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            select = 4;
        }));
    }

    // はいを押したらゲームシーンに移行
    public void AkaneOK1P()
    {
        choice = 2;
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            // 1Pが選んだキャラは2Pは押せない
            buttonA.interactable = false;
            // GameManagerからキャラのデータを取得
            GameManager.Instance.ChoiseChar_1P = 0;
            text1P.SetActive(false);
            text2P.SetActive(true);
            Image1P.SetActive(false);
            Image2P.SetActive(true);
            select = 4;
        }));
    }
    public void AkaneOK2P()
    {
        choice = 8;
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            GameManager.Instance.ChoiseChar_2P = 0;
            SceneManager.LoadScene("SenkouKime");
        }));
    }

    public void IoriCancel1P()
    {
        choice = 3;
        // 1.5秒後にシーン(画像)に移行
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            select = 0;
        }));
    }
    public void IoriCancel2P()
    {
        choice = 9;
        // 1.5秒後にシーン(画像)に移行
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            select = 4;
        }));
    }

    public void IoriOK1P()
    {
        choice = 4;
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            buttonI.interactable = false;
            GameManager.Instance.ChoiseChar_1P = 2;
            text1P.SetActive(false);
            text2P.SetActive(true);
            Image1P.SetActive(false);
            Image2P.SetActive(true);
            select = 4;
        }));
    }
    public void IoriOK2P()
    {
        choice = 10;
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            GameManager.Instance.ChoiseChar_2P = 2;
            SceneManager.LoadScene("SenkouKime");
        }));
    }

    public void NanaCancel1P()
    {
        choice = 5;
        // 1.5秒後にシーン(画像)に移行
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            select = 0;
        }));
    }
    public void NanaCancel2P()
    {
        choice = 11;
        // 1.5秒後にシーン(画像)に移行
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            select = 4;
        }));
    }

    public void NanaOK1P()
    {
        choice = 6;
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            buttonN.interactable = false;
            GameManager.Instance.ChoiseChar_1P = 1;
            text1P.SetActive(false);
            text2P.SetActive(true);
            Image1P.SetActive(false);
            Image2P.SetActive(true);
            select = 4;
        }));
        
    }
    public void NanaOK2P()
    {
        choice = 12;
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            GameManager.Instance.ChoiseChar_2P = 1;
            SceneManager.LoadScene("SenkouKime");
        }));
    }

    /// <summary>
    /// 渡された処理を指定時間後に実行する
    /// </summary>
    /// <param name="waitTime">遅延時間[ミリ秒]</param>
    /// <param name="action">実行したい処理</param>
    /// <returns></returns>
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    void Start()
    {
        // 各キャラのボタンを取得
        buttonA = GameObject.Find("Select/CharacterSelect2P/ButtonA").GetComponent<Button>();
        buttonI = GameObject.Find("Select/CharacterSelect2P/ButtonI").GetComponent<Button>();
        buttonN = GameObject.Find("Select/CharacterSelect2P/ButtonN").GetComponent<Button>();
        // 1P,2Pの選択中を表示、非表示する
        text1P.SetActive(true);
        text2P.SetActive(false);
        Image1P.SetActive(true);
        Image2P.SetActive(false);
    }

    void Update()
    {
        for (int i = 0; i < Character.Length; i++)
        {
            Character[i].SetActive(i == select); 
        }
        for (int j = 0; j < Decision.Length; j++)
        {
            Decision[j].SetActive(j == choice);
        }
    }
}
