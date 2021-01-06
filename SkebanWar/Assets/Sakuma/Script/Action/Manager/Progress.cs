using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ゲームの進行状態を管理するクラス
/// </summary>
public class Progress : MonoBehaviour
{
    public enum GameMode
    {
        Start,
        P1Select,
        P2Select,
        Battle,
        End,
        Interval,
    }
    static public Progress Instance;

    //現在のゲームモード
    public GameMode gameMode=GameMode.Start;

    //インターバル中にゲームモードを保管しておく場所
    GameMode nextGameMode = GameMode.Start;

    //ゲームモード変更からの経過時間
    [SerializeField]
    float time = 0;

    //インターバルの時間
    float intervalTime = 0;

    //入力街を持つゲームモードの終了フラグ
    public bool endGameMode=false ;

    //バトルに分岐するかどうかのフラグ
    public bool battleFlg = false;

    //バトル終了後のターンプレイヤーの保存
    public int afterBattleTrnePlayer = 0;

    ///外部マネージャー
    [SerializeField]
    DeckManager P1deckManager;
    [SerializeField]
    DeckManager P2deckManager;


    void Start()
    {
        Instance = this;
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
            case GameMode.Battle:
                break;
            case GameMode.End:
                break;
            case GameMode.Interval:
                Instance.IntervalUpdate();
                break;
        }
        //Debug.Log(gameMode);
    }

    void StartUpdate()
    {
        if (time > 1)
        {
            ChagngeGameMode(GameMode.P1Select,1f);
        }
    }

    void P1SelectUpdate()
    {
        if(endGameMode)
        {
            if (!battleFlg)
            {
                ChagngeGameMode(GameMode.P2Select, 1f);
            }
            else
            {
                afterBattleTrnePlayer = 2;
                ChagngeGameMode(GameMode.Battle , 0.5f);
            }
            
        }
    }

    void P2SelectUpdate()
    {
        if (endGameMode)
        {
            if (!battleFlg)
            {
                ChagngeGameMode(GameMode.P1Select, 1f);
            }
            else
            {
                afterBattleTrnePlayer = 1;
                ChagngeGameMode(GameMode.Battle, 0.5f);
            }
        }
    }

    public void ChagngeGameMode(GameMode changeGameMode,float intervalSet=0)
    {
        endGameMode = false;
        nextGameMode = changeGameMode;
        gameMode = GameMode.Interval;
        intervalTime = intervalSet;
        time = 0;
    }

    void P1SelectStart()
    {
        battleFlg = false;
        P1deckManager.Draw();

        if (P1deckManager.OnHandLess())
        {
            if (P2deckManager.OnHandLess())
            {
                ChagngeGameMode(GameMode.End , 0f);
            }
            else
            {
                ChagngeGameMode(GameMode.P2Select , 0f);
            }
        }

    }
    void P2SelectStart()
    {
        battleFlg = false;
        P2deckManager.Draw();

        if (P2deckManager.OnHandLess())
        {
            if (P1deckManager.OnHandLess())
            {
                ChagngeGameMode(GameMode.End, 0f);
            }
            else
            {
                ChagngeGameMode(GameMode.P1Select, 0f);
            }
        }

    }

    void IntervalUpdate()
    {
        if(time >= intervalTime)
        {
            gameMode = nextGameMode;
            time = 0;

            switch (gameMode)
            {
                case GameMode.P1Select:
                    P1SelectStart();
                    break;
                case GameMode.P2Select:
                    P2SelectStart();
                    break;
                case GameMode.Battle:
                    break;
                case GameMode.End:
                    break;
                case GameMode.Interval:
                    break;
            }
        }
    }


}
