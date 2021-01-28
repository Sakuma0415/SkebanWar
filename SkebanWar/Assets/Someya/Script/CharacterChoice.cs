using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterChoice : MonoBehaviour
{
    public enum GameMode
    {
        Start,
        P1Select,
        P2Select,
        End,
        Interval,
    }
    static public CharacterChoice Instance;

    //現在のゲームモード
    public GameMode gameMode = GameMode.Start;

    //インターバル中にゲームモードを保管しておく場所
    GameMode nextGameMode = GameMode.Start;

    //ゲームモード変更からの経過時間
    [SerializeField]
    float time = 0;

    //インターバルの時間
    float intervalTime = 0;
    [SerializeField]
    int select = 0;

    [SerializeField]
    GameObject[] Character;

    [SerializeField]
    int choice = 0;

    [SerializeField]
    GameObject[] Decision;

    Button buttonA;
    Button buttonI;
    Button buttonN;

    bool moveCan1P = false;
    bool moveCan2P = false;

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
        if(gameMode == GameMode.P1Select)
        {
            StartCoroutine(DelayMethod(1.5f, () =>
            {
                moveCan1P = true;
                buttonA.interactable = false;
                GameManager.Instance.ChoiseChar_1P = 1;
                select = 0;
            }));
        }
        if (gameMode == GameMode.P2Select)
        {
            StartCoroutine(DelayMethod(1.5f, () =>
            {
                moveCan2P = true;
                GameManager.Instance.ChoiseChar_2P = 1;
                SceneManager.LoadScene("SenkouKime");
            }));
        }
        /*
         * 1Pが選んだキャラをGameManager.Instance.ChoiseChar_1Pに、
         * 2Pが選んだキャラをGameManager.Instance.ChoiseChar_2Pに入れてほしい。
         * 1 = Akane, 2 = Nana, 3 = Ioriでお願いします。
        */
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
        if (gameMode == GameMode.P1Select)
        {
            StartCoroutine(DelayMethod(1.5f, () =>
            {
                moveCan1P = true;
                buttonI.interactable = false;
                GameManager.Instance.ChoiseChar_1P = 3;
                choice = 0;
            }));
        }
        if (gameMode == GameMode.P2Select)
        {
            StartCoroutine(DelayMethod(1.5f, () =>
            {
                moveCan2P = true;
                GameManager.Instance.ChoiseChar_2P = 3;
                SceneManager.LoadScene("SenkouKime");
            }));
        }

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
        if (gameMode == GameMode.P1Select)
        {
            StartCoroutine(DelayMethod(1.5f, () =>
            {
                moveCan1P = true;
                buttonN.interactable = false;
                GameManager.Instance.ChoiseChar_1P = 2;
                SceneManager.LoadScene("SenkouKime");
            }));
        }
        if (gameMode == GameMode.P2Select)
        {
            StartCoroutine(DelayMethod(1.5f, () =>
            {
                moveCan2P = true;
                GameManager.Instance.ChoiseChar_2P = 2;
                SceneManager.LoadScene("SenkouKime");
            }));
        }
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
        Instance = this;
        buttonA = GameObject.Find("Select/CharacterSelect/ButtonA").GetComponent<Button>();
        buttonI = GameObject.Find("Select/CharacterSelect/ButtonI").GetComponent<Button>();
        buttonN = GameObject.Find("Select/CharacterSelect/ButtonN").GetComponent<Button>();
    }

    void Update()
    {
        Instance.time += Time.deltaTime;

        switch (gameMode)
        {
            case GameMode.Start:
                Instance.StartUpdate();
                break;
            case GameMode.P1Select:
                Instance.P1SelectUpdate();
                break;
            case GameMode.P2Select:
                Instance.P2SelectUpdate();
                break;
            case GameMode.End:
                EndUpdate();
                break;
            case GameMode.Interval:
                Instance.IntervalUpdate();
                break;
        }
        Debug.Log(gameMode);
        for (int i = 0; i < Character.Length; i++)
        {
            Character[i].SetActive(i == select); 
        }
        for (int j = 0; j < Decision.Length; j++)
        {
            Decision[j].SetActive(j == choice);
        }
    }

    private void StartUpdate()
    {
        if (time > 1)
        {
            ChagngeGameMode(GameMode.P1Select, 1f);
        }
    }
    private void P1SelectUpdate()
    {
        if(moveCan1P)
        {
            ChagngeGameMode(GameMode.P2Select, 1f);
        }
    }
    private void P2SelectUpdate()
    {
        if (moveCan2P)
        {
            ChagngeGameMode(GameMode.End, 1f);
        }
    }
    private void EndUpdate()
    {
        
    }

    public void ChagngeGameMode(GameMode changeGameMode, float intervalSet = 0)
    {
        moveCan1P = false;
        moveCan2P = false;
        nextGameMode = changeGameMode;
        gameMode = GameMode.Interval;
        intervalTime = intervalSet;
        time = 0;
    }

    void IntervalUpdate()
    {
        if (time >= intervalTime)
        {
            gameMode = nextGameMode;
            time = 0;

            switch (gameMode)
            {
                case GameMode.P1Select:
                    break;
                case GameMode.P2Select:
                    break;
                case GameMode.End:
                    break;
                case GameMode.Interval:
                    break;
            }
        }
    }
}
