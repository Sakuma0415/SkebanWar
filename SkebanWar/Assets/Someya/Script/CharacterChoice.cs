using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterChoice : MonoBehaviour
{
    [SerializeField]
    int select = 0;

    [SerializeField]
    GameObject[] Character;

    [SerializeField]
    int choice = 0;

    [SerializeField]
    GameObject[] Decision;

    // ボタンを押したら選んだキャラクターを描画
    public void AkaneChange()
    {
        choice = 0;
        select = 1;
    }

    public void IoriChange()
    {
        choice = 0;
        select = 2;
    }

    public void NanaChange()
    {
        choice = 0;
        select = 3;
    }

    // いいえを押したらキャラクター選択に戻る
    public void AkaneCancel()
    {
        choice = 1;
        // 1.5秒後にシーン(画像)に移行
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            select = 0;
        }));
    }

    // はいを押したらゲームシーンに移行
    public void AkaneOK()
    {
        choice = 2;
        /*
         * 1Pが選んだキャラをGameManager.Instance.ChoiseChar_1Pに、
         * 2Pが選んだキャラをGameManager.Instance.ChoiseChar_2Pに入れてほしい。
         * 0 = Akane, 1 = Nana, 2 = Ioriでお願いします。
        */

        StartCoroutine(DelayMethod(1.5f, () =>
        {
            GameManager.Instance.ChoiseChar_1P = 0;
            GameManager.Instance.ChoiseChar_2P = 1;
            SceneManager.LoadScene("SenkouKime");
        }));
    }

    public void IoriCancel()
    {
        choice = 3;
        // 1.5秒後にシーン(画像)に移行
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            select = 0;
        }));
    }

    public void IoriOK()
    {
        choice = 4;
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            GameManager.Instance.ChoiseChar_1P = 2;
            GameManager.Instance.ChoiseChar_2P = 0;
            SceneManager.LoadScene("SenkouKime");
        }));
    }

    public void NanaCancel()
    {
        choice = 5;
        // 1.5秒後にシーン(画像)に移行
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            select = 0;
        }));
    }

    public void NanaOK()
    {
        choice = 6;
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            GameManager.Instance.ChoiseChar_1P = 1;
            GameManager.Instance.ChoiseChar_2P = 2;
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
