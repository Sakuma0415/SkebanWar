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
    public void C1Change()
    {
        choice = 0;
        select = 1;
    }

    public void C2Change()
    {
        choice = 0;
        select = 2;
    }

    public void C3Change()
    {
        choice = 0;
        select = 3;
    }

    // いいえを押したらキャラクター選択に戻る
    public void C1Cancel()
    {
        choice = 1;
        // 1.5秒後にシーン(画像)に移行
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            select = 0;
        }));
    }

    // はいを押したらゲームシーンに移行
    public void C1OK()
    {
        choice = 2;
        //StartCoroutine(DelayMethod(1.5f, () =>
        //{
        //    SceneManager.LoadScene("Action");
        //}));
    }
    
    public void C2Cancel()
    {
        choice = 3;
        // 1.5秒後にシーン(画像)に移行
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            select = 0;
        }));
    }

    public void C2OK()
    {
        choice = 4;
        //StartCoroutine(DelayMethod(1.5f, () =>
        //{
        //    SceneManager.LoadScene("Action");
        //}));
    }

    public void C3Cancel()
    {
        choice = 5;
        // 1.5秒後にシーン(画像)に移行
        StartCoroutine(DelayMethod(1.5f, () =>
        {
            select = 0;
        }));
    }

    public void C3OK()
    {
        choice = 6;
        //StartCoroutine(DelayMethod(1.5f, () =>
        //{
        //    SceneManager.LoadScene("Action");
        //}));
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
