﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    public enum BattleProcess
    {
        Start,
        FirstDice,
        FirstAttack,
        SecondDice,
        SecondAttack,
        CutIn,
        End,
        Interval,
    }

    static public Battle Instance;
    //現在のゲームモード
    public BattleProcess nowProcess = BattleProcess.Start;

    //インターバル中にゲームモードを保管しておく場所
    BattleProcess nextGameMode = BattleProcess.Start;

    //ゲームモード変更からの経過時間
    [SerializeField]
    float time = 0;

    //インターバルの時間
    float intervalTime = 0;

    //入力を持つゲームモードの終了フラグ
    public bool endGameMode = false;

    private bool diceBool = true;
    [SerializeField]
    private Text diceText;
    [SerializeField]
    private GameObject diceButton;
    private bool doOnce = true;
    private int diceNumber;

    //キャラクターデータ
    [SerializeField]
    private CharDataBase charData;
    private CharacterData lowerChar;
    private CharacterData upperChar;

    //スプライトデータ
    [SerializeField]
    private SpriteData spriteData;

    //上、下側のキャライメージ
    private Image upperImage;
    private Image lowerImage;

    //上、下側のキャラHP
    [SerializeField]
    private Text upperText;
    [SerializeField]
    private Text lowerText;

    //喧嘩上等 時の左右のキャラクターイメージ定義
    private Image kenkaImage_Left;
    private Image kenkaImage_Right;

    //喧嘩上等 時の左右のプレートイメージ定義
    private Image plateLeft;
    private Image plateRight;

    [SerializeField]
    private CanvasGroup fadeCanvas;

    //アニメーター定義
    private Animator anim_Icon;
    private Animator anim_Plate;
    private Animator anim_Text;

    //先攻後攻決め(true=1P,false=2P)
    private bool witchAttackBool = false;

    void Start()
    {
        Instance = this;
        fadeCanvas.alpha = 0;

        //アニメーター初期化
        anim_Icon = GameObject.FindGameObjectWithTag("IconAnim").GetComponent<Animator>();
        anim_Plate = GameObject.FindGameObjectWithTag("PlateAnim").GetComponent<Animator>();
        anim_Text = GameObject.FindGameObjectWithTag("TextAnim").GetComponent<Animator>();

        //キャラクターアイコン初期化
        upperImage = GameObject.FindGameObjectWithTag("UpperImage").GetComponent<Image>();
        lowerImage = GameObject.FindGameObjectWithTag("LowerImage").GetComponent<Image>();

        //喧嘩上等 時の左右のキャラクターイメージ初期化
        kenkaImage_Left = GameObject.FindGameObjectWithTag("PlateImageLeft").GetComponent<Image>();
        kenkaImage_Right = GameObject.FindGameObjectWithTag("PlateImageRight").GetComponent<Image>();

        //喧嘩上等 時の左右のプレートイメージ初期化
        plateLeft = GameObject.FindGameObjectWithTag("LeftPlate").GetComponent<Image>();
        plateRight = GameObject.FindGameObjectWithTag("RightPlate").GetComponent<Image>();

        //キャラクター情報追加
        upperChar = charData.characterDatas[0];
        lowerChar = charData.characterDatas[8];

        //デバッグ用
        upperChar.HP = 7;
        lowerChar.HP = 7;

        //ImageとHPを表示
        upperImage.sprite = upperChar.Image;
        upperText.text = upperChar.HP.ToString();
        lowerImage.sprite = lowerChar.Image;
        lowerText.text = lowerChar.HP.ToString();

        //先攻後攻で画像を分ける
        if (witchAttackBool)
        {
            plateLeft.sprite = spriteData.Sprites[2];
            plateRight.sprite = spriteData.Sprites[1];
            kenkaImage_Left.sprite = lowerChar.IconImage;
            kenkaImage_Right.sprite = upperChar.IconImage;
        }
        else
        {
            plateLeft.sprite = spriteData.Sprites[0];
            plateRight.sprite = spriteData.Sprites[3];
            kenkaImage_Left.sprite = upperChar.IconImage;
            kenkaImage_Right.sprite = lowerChar.IconImage;
        }

        //サイコロ時キャラ画像を消すか消さないか
        //upperImage.gameObject.SetActive(false);
        //lowerImage.gameObject.SetActive(false);        
    }

    // Update is called once per frame
    void Update()
    {
        Instance.time += Time.deltaTime;

        switch (nowProcess)
        {
            case BattleProcess.Start:
                Instance.StartUpdate();
                break;
            case BattleProcess.FirstDice:
                Instance.DiceShake();
                break;
            case BattleProcess.FirstAttack:
                Instance.HPCalculation();
                break;
            case BattleProcess.SecondDice:
                Instance.DiceShake();
                break;
            case BattleProcess.SecondAttack:
                Instance.HPCalculation();
                break;
            case BattleProcess.CutIn:
                break;
            case BattleProcess.End:
                Instance.EndPhase();
                break;
            case BattleProcess.Interval:
                Instance.IntervalUpdate();
                break;
        }
        Debug.Log(nowProcess);
    }
    void StartUpdate()
    {
        //if (time > 1)
        //{
        //    ChagngeGameMode(BattleProcess.FirstDice, 1f);
        //}
        if (doOnce)
        {
            doOnce = false;
            StartCoroutine(FadeOut(1.0f));
        }        
    }
    private IEnumerator FadeOut(float fadeTime)
    {
        float time = 0f;
        while (fadeCanvas.alpha < 1)
        {
            time += Time.deltaTime;
            fadeCanvas.alpha = 1f * (time / fadeTime);
            yield return null;
        }

        anim_Plate.SetTrigger("Plates");
        yield return new WaitForSeconds(anim_Plate.GetCurrentAnimatorStateInfo(0).length);

        anim_Icon.SetTrigger("Icons");
        yield return new WaitForSeconds(anim_Icon.GetCurrentAnimatorStateInfo(0).length);

        anim_Text.SetTrigger("KenkaTexts");        
        yield return new WaitForSeconds(anim_Text.GetCurrentAnimatorStateInfo(0).length + 1.2f);

        while (kenkaImage_Right.color.r < 1)
        {
            time += Time.deltaTime;
            kenkaImage_Right.color = new Color(0.4f * (time / fadeTime), 0.4f * (time / fadeTime), 0.4f * (time / fadeTime));
            yield return null;
        }
        yield return new WaitForSeconds(2.0f);

        //ChagngeGameMode(BattleProcess.FirstDice, 1f);

        yield break;
    }

    private void DiceShake()
    {
        if (!diceBool) return;

        if (doOnce)
        {
            doOnce = false;
            diceText.gameObject.SetActive(true);
            diceButton.gameObject.SetActive(true);
        }        
        diceText.text = Random.Range(1, 6).ToString();
    }
    public void DiceButton()
    {
        diceBool = false;
        diceNumber = Random.Range(1, 6);
        Debug.Log(diceNumber);
        diceText.text = diceNumber.ToString();
        switch(nowProcess){
            case BattleProcess.FirstDice:
                ChagngeGameMode(BattleProcess.FirstAttack, 1f);
                break;
            case BattleProcess.SecondDice:
                ChagngeGameMode(BattleProcess.SecondAttack, 1f);
                break;
        }
        
    }

    private void HPCalculation()
    {
        if (doOnce)
        {
            doOnce = false;
            switch (nowProcess)
            {
                case BattleProcess.FirstAttack:
                    TypeMatchLower();
                    upperChar.HP -= diceNumber;
                    if (upperChar.HP <= 0)
                    {
                        upperChar.HP = 0;
                        ChagngeGameMode(BattleProcess.End, 1f);
                    }
                    upperText.text = upperChar.HP.ToString();
                    ChagngeGameMode(BattleProcess.SecondDice, 1f);
                    break;

                case BattleProcess.SecondAttack:
                    TypeMatchUpper();
                    lowerChar.HP -= diceNumber;
                    if (lowerChar.HP <= 0)
                    {
                        lowerChar.HP = 0;
                    }
                    lowerText.text = lowerChar.HP.ToString();
                    ChagngeGameMode(BattleProcess.End, 1f);
                    break;
            }
        }        
    }

    private void EndPhase()
    {

    }
    public void ChagngeGameMode(BattleProcess changeGameMode, float intervalSet = 0)
    {
        endGameMode = false;
        nextGameMode = changeGameMode;
        nowProcess = BattleProcess.Interval;
        intervalTime = intervalSet;
        time = 0;
    }

    void IntervalUpdate()
    {
        if (time >= intervalTime)
        {
            nowProcess = nextGameMode;
            time = 0;

            switch (nowProcess)
            {
                case BattleProcess.FirstDice:
                    doOnce = true;
                    break;
                case BattleProcess.FirstAttack:
                    doOnce = true;
                    diceBool = true;
                    diceText.gameObject.SetActive(false);
                    diceButton.gameObject.SetActive(false);
                    //サイコロ時キャラ画像を消すか消さないか
                    //upperImage.gameObject.SetActive(true);
                    //lowerImage.gameObject.SetActive(true);
                    break;
                case BattleProcess.SecondDice:
                    doOnce = true;
                    break;
                case BattleProcess.SecondAttack:
                    doOnce = true;
                    break;
                case BattleProcess.CutIn:
                    break;
                case BattleProcess.End:
                    break;
                case BattleProcess.Interval:
                    break;
            }
        }
    }
    private void TypeMatchLower()
    {
        if(lowerChar.Color == CharacterData.CharColor.Yellow && upperChar.Color == CharacterData.CharColor.Blue)
        {
            diceNumber += 1;
        }

        if (lowerChar.Color == CharacterData.CharColor.Blue && upperChar.Color == CharacterData.CharColor.Red)
        {
            diceNumber += 1;
        }

        if (lowerChar.Color == CharacterData.CharColor.Red && upperChar.Color == CharacterData.CharColor.Yellow)
        {
            diceNumber += 1;
        }
    }
    private void TypeMatchUpper()
    {
        if (upperChar.Color == CharacterData.CharColor.Yellow && lowerChar.Color == CharacterData.CharColor.Blue)
        {
            diceNumber += 1;
        }

        if (upperChar.Color == CharacterData.CharColor.Blue && lowerChar.Color == CharacterData.CharColor.Red)
        {
            diceNumber += 1;
        }

        if (upperChar.Color == CharacterData.CharColor.Red && lowerChar.Color == CharacterData.CharColor.Yellow)
        {
            diceNumber += 1;
        }
    }
}

