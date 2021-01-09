﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    public enum BattleProcess
    {
        Start,
        FirstDice,
        FirstAttack,
        CutIn,
        SecondDice,
        SecondAttack,
        ReRollChance,
        End,
        Interval,
    }

    static public Battle Instance;
    //現在のゲームモード
    public BattleProcess nowProcess = BattleProcess.Start;

    //インターバル中にゲームモードを保管しておく場所
    BattleProcess nextGameMode = BattleProcess.Start;
    //1個前のゲームモード保管
    BattleProcess beforeProcess = BattleProcess.Start;

    //ゲームモード変更からの経過時間
    [SerializeField]
    float time = 0;

    //インターバルの時間
    float intervalTime = 0;

    //入力を持つゲームモードの終了フラグ
    public bool endGameMode = false;
    private bool diceBool = true;

    //仮実装用
    [SerializeField]
    private Text diceText;
    [SerializeField]
    private GameObject diceButton;

    //本実装用
    [SerializeField]
    private Image rollTheDice;
    [SerializeField]
    private Image diceArrow;

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
    private Animator anim_Attack;
    private Animator anim_FlashUpper;
    private Animator anim_FlashLower;
    private Animator anim_CutInMask;

    //先攻後攻決め(true=1P,false=2P)
    private bool witchAttackBool = true;

    private bool cutInBool;
    private Touch touch = Input.GetTouch(0);
    [SerializeField]
    private GameObject nextPlayerTexts;
    [SerializeField]
    private GameObject CutInImage;
    [SerializeField]
    private Image reRollImage;
    private Button yesButton;
    private Button noButton;
    private bool rerollDiceBool = true;
    [SerializeField]
    private Material grayScale;

    void Start()
    {
        Instance = this;
        fadeCanvas.alpha = 0;        

        //アニメーター初期化
        anim_Icon = GameObject.FindGameObjectWithTag("IconAnim").GetComponent<Animator>();
        anim_Plate = GameObject.FindGameObjectWithTag("PlateAnim").GetComponent<Animator>();
        anim_Text = GameObject.FindGameObjectWithTag("TextAnim").GetComponent<Animator>();
        anim_Attack = GameObject.FindGameObjectWithTag("AttackAnim").GetComponent<Animator>();
        anim_FlashUpper = GameObject.FindGameObjectWithTag("UpperImage").GetComponent<Animator>();
        anim_FlashLower = GameObject.FindGameObjectWithTag("LowerImage").GetComponent<Animator>();
        anim_CutInMask = GameObject.FindGameObjectWithTag("CutInMask").GetComponent<Animator>();

        //ボタン初期化
        yesButton = GameObject.FindGameObjectWithTag("YesButton").GetComponent<Button>();
        noButton = GameObject.FindGameObjectWithTag("NoButton").GetComponent<Button>();
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);

        //キャラクターアイコン初期化
        upperImage = GameObject.FindGameObjectWithTag("UpperImage").GetComponent<Image>();
        lowerImage = GameObject.FindGameObjectWithTag("LowerImage").GetComponent<Image>();
        upperImage.gameObject.SetActive(false);
        lowerImage.gameObject.SetActive(false);

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
        upperChar.HP = 1;
        lowerChar.HP = 1;
        GameManager.Instance.HaveCoins_1P += 4;
        GameManager.Instance.HaveCoins_2P += 4;

        //ImageとHPを表示
        upperImage.sprite = upperChar.Image;
        upperText.text = upperChar.HP.ToString();
        lowerImage.sprite = lowerChar.Image;
        lowerText.text = lowerChar.HP.ToString();
        CutInImage.GetComponent<SpriteRenderer>().sprite = upperChar.CutInImage;

        //先攻後攻で画像を分ける
        if (witchAttackBool)
        {
            plateLeft.sprite = spriteData.Sprites[2];
            plateRight.sprite = spriteData.Sprites[1];
            kenkaImage_Left.sprite = lowerChar.IconImage;
            kenkaImage_Right.sprite = upperChar.IconImage;
            //1P側に向けて表示
            anim_Icon.transform.rotation = Quaternion.Euler(0, 0, 0);
            anim_Plate.transform.rotation = Quaternion.Euler(0, 0, 0);
            anim_Text.transform.rotation = Quaternion.Euler(0, 0, 0);
            rollTheDice.transform.rotation = Quaternion.Euler(0, 0, 0);
            diceArrow.transform.rotation = Quaternion.Euler(0, 0, 0);
            reRollImage.transform.rotation = Quaternion.Euler(0, 0, 0);
            yesButton.transform.rotation = Quaternion.Euler(0, 0, 0);
            noButton.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (!witchAttackBool)
        {
            plateLeft.sprite = spriteData.Sprites[0];
            plateRight.sprite = spriteData.Sprites[3];
            kenkaImage_Left.sprite = upperChar.IconImage;
            kenkaImage_Right.sprite = lowerChar.IconImage;
            //2P側に向けて表示
            anim_Icon.transform.rotation = Quaternion.Euler(0, 0, 180);
            anim_Plate.transform.rotation = Quaternion.Euler(0, 0, 180);
            anim_Text.transform.rotation = Quaternion.Euler(0, 0, 180);
            rollTheDice.transform.rotation = Quaternion.Euler(0, 0, 180);
            diceArrow.transform.rotation = Quaternion.Euler(0, 0, 180);
            reRollImage.transform.rotation = Quaternion.Euler(0, 0, 180);
            yesButton.transform.rotation = Quaternion.Euler(0, 0, 180);
            noButton.transform.rotation = Quaternion.Euler(0, 0, 180);
        }     
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
            case BattleProcess.CutIn:
                Instance.CharCutIn();
                break;
            case BattleProcess.SecondDice:
                Instance.DiceShake();
                break;
            case BattleProcess.SecondAttack:
                Instance.HPCalculation();
                break;
            case BattleProcess.ReRollChance:
                Instance.ReRoll();
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
            kenkaImage_Right.color = new Color(0.25f * (time / fadeTime), 0.25f * (time / fadeTime), 0.25f * (time / fadeTime));
            yield return null;
        }
        yield return new WaitForSeconds(2.0f);

        ChagngeGameMode(BattleProcess.FirstDice, 1f);

        yield break;
    }

    private void DiceShake()
    {
        if (!diceBool) return;

        if (doOnce)
        {
            doOnce = false;
        }        
        diceText.text = Random.Range(1, 6).ToString();

        //本実装用(ifのなかにサイコロ終わった判定を入れると動くはず)
        //if ()
        //{
        //    diceBool = false;
        //    switch (nowProcess)
        //    {
        //        case BattleProcess.FirstDice:
        //            FirstShake();
        //            break;
        //        case BattleProcess.SecondDice:
        //            SecondShake();
        //            break;
        //    }
        //}
    }
    //仮実装用
    public void DiceButton()
    {
        diceBool = false;
        diceNumber = Random.Range(1, 6);
        diceText.text = diceNumber.ToString();
        switch (nowProcess)
        {
            case BattleProcess.FirstDice:
                FirstShake();
                break;
            case BattleProcess.SecondDice:
                SecondShake();
                break;
        }
    }

    private void ReRoll()
    {
        if (doOnce)
        {
            doOnce = false;
            reRollImage.gameObject.SetActive(true);
            yesButton.gameObject.SetActive(true);
            noButton.gameObject.SetActive(true);
            diceText.gameObject.SetActive(false);
            diceButton.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && !doOnce)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)Input.mousePosition, (Vector2)ray.direction);
            if (hit2d.collider.tag == "YesButton")
            {
                diceBool = true;
                reRollImage.gameObject.SetActive(false);
                yesButton.gameObject.SetActive(false);
                noButton.gameObject.SetActive(false);
                diceText.gameObject.SetActive(true);
                diceButton.gameObject.SetActive(true);
                if(beforeProcess == BattleProcess.FirstDice)
                {
                    ChagngeGameMode(BattleProcess.FirstDice, 1f);
                    if (witchAttackBool)
                    {
                        GameManager.Instance.HaveCoins_1P -= 2;
                    }

                    if (!witchAttackBool)
                    {
                        GameManager.Instance.HaveCoins_2P -= 2;
                    }
                }
                if (beforeProcess == BattleProcess.SecondDice)
                {
                    ChagngeGameMode(BattleProcess.SecondDice, 1f);
                    if (witchAttackBool)
                    {
                        GameManager.Instance.HaveCoins_2P -= 2;
                    }

                    if (!witchAttackBool)
                    {
                        GameManager.Instance.HaveCoins_1P -= 2;
                    }
                }
            }

            if (hit2d.collider.tag == "NoButton")
            {
                reRollImage.gameObject.SetActive(false);
                yesButton.gameObject.SetActive(false);
                noButton.gameObject.SetActive(false);
                ChagngeGameMode(BattleProcess.FirstAttack, 1f);
            }
        }
    }

    private void HPCalculation()
    {
        if (doOnce)
        {
            doOnce = false;
            StartCoroutine(WaitTime(1.0f));            
        }        
    }

    private IEnumerator WaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        switch (nowProcess)
        {
            case BattleProcess.FirstAttack:
                TypeMatchLower();
                AttackAnimPos();
                while (diceNumber > 0)
                {
                    AttackAnim();
                    yield return new WaitForSeconds(1f);
                    if (witchAttackBool)
                    {
                        upperChar.HP--;
                        upperText.text = upperChar.HP.ToString();
                    }
                    if(!witchAttackBool)
                    {
                        lowerChar.HP--;
                        lowerText.text = lowerChar.HP.ToString();
                    }                    
                    diceNumber--;
                    yield return new WaitForSeconds(anim_Attack.GetCurrentAnimatorStateInfo(0).length);

                    if (upperChar.HP == 0 || lowerChar.HP == 0)
                    {                        
                        if (witchAttackBool)
                        {
                            upperImage.material = grayScale;
                        }
                        if (!witchAttackBool)
                        {
                            lowerImage.material = grayScale;
                        }
                        ChagngeGameMode(BattleProcess.End, 1f);
                        yield break;
                    }

                    yield return null;                    
                }

                ChagngeGameMode(BattleProcess.CutIn, 3f);
                break;

            case BattleProcess.SecondAttack:
                TypeMatchUpper();
                AttackAnimPos();
                while (diceNumber >= 0)
                {
                    AttackAnim();
                    yield return new WaitForSeconds(1f);
                    if (!witchAttackBool)
                    {
                        upperChar.HP--;
                        upperText.text = upperChar.HP.ToString();
                    }
                    if (witchAttackBool)
                    {
                        lowerChar.HP--;
                        lowerText.text = lowerChar.HP.ToString();
                    }
                    diceNumber--;
                    yield return new WaitForSeconds(anim_Attack.GetCurrentAnimatorStateInfo(0).length);
                    if (upperChar.HP == 0 || lowerChar.HP == 0)
                    {
                        if (!witchAttackBool)
                        {
                            upperImage.material = grayScale;
                        }
                        if (witchAttackBool)
                        {
                            lowerImage.material = grayScale;
                        }
                        ChagngeGameMode(BattleProcess.End, 1f);
                        yield break;
                    }

                    yield return null;
                }

                ChagngeGameMode(BattleProcess.End, 1f);
                break;
        }
        yield break;
    }

    private void CharCutIn()
    {
        if (/*touch.phase == TouchPhase.Began ||*/ Input.GetKeyDown(KeyCode.Q))
        {
            nextPlayerTexts.gameObject.SetActive(false);
            if (doOnce)
            {
                doOnce = false;
                StartCoroutine(CutInCor(2.0f));
            }
        }     
        
    }

    private IEnumerator CutInCor(float waitTime)
    {        
        anim_CutInMask.SetTrigger("CutInMask");
        yield return new WaitForSeconds(anim_CutInMask.GetCurrentAnimatorStateInfo(0).length + waitTime);
        ChagngeGameMode(BattleProcess.SecondDice, 1f);
        yield break;
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
                    anim_Icon.gameObject.SetActive(false);
                    anim_Plate.gameObject.SetActive(false);
                    anim_Text.gameObject.SetActive(false);

                    //仮実装用
                    diceText.gameObject.SetActive(true);
                    diceButton.gameObject.SetActive(true);
                    //本実装用
                    //rollTheDice.gameObject.SetActive(true);
                    //diceArrow.gameObject.SetActive(true);
                    break;
                case BattleProcess.FirstAttack:
                    doOnce = true;
                    diceBool = true;

                    //仮実装用
                    diceText.gameObject.SetActive(false);
                    diceButton.gameObject.SetActive(false);
                    //本実装用
                    //rollTheDice.gameObject.SetActive(false);
                    //diceArrow.gameObject.SetActive(false);

                    upperImage.gameObject.SetActive(true);
                    lowerImage.gameObject.SetActive(true);
                    break;
                case BattleProcess.CutIn:
                    doOnce = true;
                    upperImage.gameObject.SetActive(false);
                    lowerImage.gameObject.SetActive(false);
                    nextPlayerTexts.gameObject.SetActive(true);
                    break;
                case BattleProcess.SecondDice:
                    doOnce = true;
                    CutInImage.gameObject.SetActive(false);
                    break;
                case BattleProcess.SecondAttack:
                    doOnce = true;
                    diceBool = true;

                    //仮実装用
                    diceText.gameObject.SetActive(false);
                    diceButton.gameObject.SetActive(false);
                    //本実装用
                    //rollTheDice.gameObject.SetActive(false);
                    //diceArrow.gameObject.SetActive(false);

                    upperImage.gameObject.SetActive(true);
                    lowerImage.gameObject.SetActive(true);
                    break;
                case BattleProcess.ReRollChance:
                    doOnce = true;
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

    private void FirstShake()
    {
        if (witchAttackBool)
        {
            if (GameManager.Instance.HaveCoins_1P > 1)
            {
                beforeProcess = BattleProcess.FirstDice;
                ChagngeGameMode(BattleProcess.ReRollChance, 1f);
            }
            else
            {
                ChagngeGameMode(BattleProcess.FirstAttack, 1f);
            }
        }
        if (!witchAttackBool)
        {
            if (GameManager.Instance.HaveCoins_2P > 1)
            {
                beforeProcess = BattleProcess.FirstDice;
                ChagngeGameMode(BattleProcess.ReRollChance, 1f);
            }
            else
            {
                ChagngeGameMode(BattleProcess.FirstAttack, 1f);
            }
        }
    }
    private void SecondShake()
    {
        if (witchAttackBool)
        {
            if (GameManager.Instance.HaveCoins_2P > 1)
            {
                beforeProcess = BattleProcess.SecondDice;
                ChagngeGameMode(BattleProcess.ReRollChance, 1f);
            }
            else
            {
                ChagngeGameMode(BattleProcess.SecondAttack, 1f);
            }
        }
        if (!witchAttackBool)
        {
            if (GameManager.Instance.HaveCoins_1P > 1)
            {
                beforeProcess = BattleProcess.SecondDice;
                ChagngeGameMode(BattleProcess.ReRollChance, 1f);
            }
            else
            {
                ChagngeGameMode(BattleProcess.SecondAttack, 1f);
            }
        }
    }

    private void AttackAnimPos()
    {
        if (witchAttackBool && nowProcess == BattleProcess.FirstAttack)
        {
            anim_Attack.transform.position = upperImage.transform.position;
        }

        if (!witchAttackBool && nowProcess == BattleProcess.FirstAttack)
        {
            anim_Attack.transform.position = lowerImage.transform.position;
        }

        if (witchAttackBool && nowProcess == BattleProcess.SecondAttack)
        {
            anim_Attack.transform.position = lowerImage.transform.position;
        }

        if (!witchAttackBool && nowProcess == BattleProcess.SecondAttack)
        {
            anim_Attack.transform.position = upperImage.transform.position;
        }
    }

    private void AttackAnim()
    {
        if (witchAttackBool && nowProcess == BattleProcess.FirstAttack)
        {
            anim_Attack.SetTrigger("Attack");
            anim_FlashUpper.SetTrigger("Flash");
        }

        if (!witchAttackBool && nowProcess == BattleProcess.FirstAttack)
        {
            anim_Attack.SetTrigger("Attack");
            anim_FlashLower.SetTrigger("FlashLower");
        }

        if (witchAttackBool && nowProcess == BattleProcess.SecondAttack)
        {
            anim_Attack.SetTrigger("Attack");
            anim_FlashLower.SetTrigger("FlashLower");
        }

        if (!witchAttackBool && nowProcess == BattleProcess.SecondAttack)
        {
            anim_Attack.SetTrigger("Attack");
            anim_FlashUpper.SetTrigger("Flash");
        }
    }
}

