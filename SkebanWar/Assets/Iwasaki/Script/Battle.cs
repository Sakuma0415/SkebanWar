using System.Collections;
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

    //本実装用
    [SerializeField]
    private Image rollTheDice;
    [SerializeField]
    private Image diceArrow;

    public bool doOnce = false ;
    private int diceNumber;

    //キャラクターデータ
    [SerializeField]
    private CharDataBase charData;
    public CharDataBaseBattle battleImage;
    public int lowerCharHP;
    public int upperCharHP;

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
    public bool witchAttackBool = true;

    private bool cutInBool;
    private Touch touch ;
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
    private Material normalMaterial;
    [SerializeField]
    private GameObject rerollButtons;



    //佐久間
    [SerializeField]
    Dice dice;
    [SerializeField]
    GameObject diceObj;
    public bool EndPass = false;
    [SerializeField]
    BattleManager battleManager;
    [SerializeField]
    private GameObject P1Deck;
    [SerializeField]
    private GameObject P2Deck;

    [SerializeField]
    Sprite[] akane;
    [SerializeField]
    Sprite[] iori;
    [SerializeField]
    Sprite[] nana;

    //攻撃側の属性
    public CharacterManager.Attribute AtkAT = CharacterManager.Attribute.None;

    //守備側の属性
    public CharacterManager.Attribute DefAT = CharacterManager.Attribute.None;

    void Start()
    {
        Instance = this;
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

        //キャラクターアイコン初期化
        upperImage = GameObject.FindGameObjectWithTag("UpperImage").GetComponent<Image>();
        lowerImage = GameObject.FindGameObjectWithTag("LowerImage").GetComponent<Image>();

        //喧嘩上等 時の左右のキャラクターイメージ初期化
        kenkaImage_Left = GameObject.FindGameObjectWithTag("PlateImageLeft").GetComponent<Image>();
        kenkaImage_Right = GameObject.FindGameObjectWithTag("PlateImageRight").GetComponent<Image>();

        //喧嘩上等 時の左右のプレートイメージ初期化
        plateLeft = GameObject.FindGameObjectWithTag("LeftPlate").GetComponent<Image>();
        plateRight = GameObject.FindGameObjectWithTag("RightPlate").GetComponent<Image>();


        Init();
    }


    void Init()
    {
        other();

        fadeCanvas.alpha = 0;
        kenkaImage_Right.color = new Color(0, 0, 0);
        upperImage.material = null;
        lowerImage.material = null;

        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);

        upperImage.gameObject.SetActive(false);
        lowerImage.gameObject.SetActive(false);        

        //HPを表示
        upperText.text = upperCharHP.ToString();
        lowerText.text = lowerCharHP.ToString();

        //先攻後攻で画像を分ける
        if (witchAttackBool)
        {
            plateLeft.sprite = spriteData.Sprites[2];
            plateRight.sprite = spriteData.Sprites[1];
            //1P側に向けて表示
            anim_Icon.transform.rotation = Quaternion.Euler(0, 0, 0);
            anim_Plate.transform.rotation = Quaternion.Euler(0, 0, 0);
            anim_Text.transform.rotation = Quaternion.Euler(0, 0, 0);
            rollTheDice.transform.rotation = Quaternion.Euler(0, 0, 0);
            diceArrow.transform.rotation = Quaternion.Euler(0, 0, 0);           
        }

        if (!witchAttackBool)
        {
            plateLeft.sprite = spriteData.Sprites[0];
            plateRight.sprite = spriteData.Sprites[3];
            //2P側に向けて表示
            anim_Icon.transform.rotation = Quaternion.Euler(0, 0, 180);
            anim_Plate.transform.rotation = Quaternion.Euler(0, 0, 180);
            anim_Text.transform.rotation = Quaternion.Euler(0, 0, 180);
            rollTheDice.transform.rotation = Quaternion.Euler(0, 0, 180);
            diceArrow.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        CutInImage.SetActive(false);
    }

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
        //Debug.Log(nowProcess);
    }
    void StartUpdate()
    {

        if(Input .GetKeyDown (KeyCode.A))
        {
            doOnce = true;
        }


        if (doOnce)
        {
            doOnce = false;

            //デッキ内のキャラを一時的に消す
            P1Deck.SetActive(false);
            P2Deck.SetActive(false);

            StartCoroutine(FadeOut(1.0f));
            EndPass = false ;
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
            kenkaImage_Right.color = new Color(0.5f * (time / fadeTime), 0.5f * (time / fadeTime), 0.5f * (time / fadeTime));
            yield return null;
        }
        //yield return new WaitForSeconds(2.0f);

        ChagngeGameMode(BattleProcess.FirstDice, 2f);

        yield break;
    }

    private void DiceShake()
    {
        float timeEnd = 0.5f;

        if (!dice.diceEnd)
        {
            time = 0;
        }

        if (timeEnd < time)
        {
            diceNumber=dice.Ans;
            dice.IsDice = false;
            diceObj.SetActive(false);
            dice.DiceSet();
            if(beforeProcess == BattleProcess.FirstDice)
            {
                FirstShake();
            }
            else
            {
                SecondShake();
            }
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
                //diceText.gameObject.SetActive(true);
                //diceButton.gameObject.SetActive(true);
                if(beforeProcess == BattleProcess.FirstDice)
                {
                    ChagngeGameMode(BattleProcess.FirstDice, 0.25f);
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
                    ChagngeGameMode(BattleProcess.SecondDice, 0.25f);
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
                //Debug.Log(beforeProcess);
                ChagngeGameMode(beforeProcess == BattleProcess.FirstDice ? BattleProcess.FirstAttack : BattleProcess.SecondAttack, 0.25f);
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
                //TypeMatchLower();
                AttackAnimPos();
                while (diceNumber > 0)
                {
                    AttackAnim();
                    yield return new WaitForSeconds(0.5f);
                    if (witchAttackBool)
                    {
                        upperCharHP--;
                        upperText.text = upperCharHP.ToString();
                    }
                    if(!witchAttackBool)
                    {
                        lowerCharHP--;
                        lowerText.text = lowerCharHP.ToString();
                    }                    
                    diceNumber--;
                    yield return new WaitForSeconds(anim_Attack.GetCurrentAnimatorStateInfo(0).length);

                    if (upperCharHP == 0 || lowerCharHP == 0)
                    {                        
                        if (witchAttackBool)
                        {
                            upperImage.material = grayScale;
                        }
                        if (!witchAttackBool)
                        {
                            lowerImage.material = grayScale;
                        }
                        ChagngeGameMode(BattleProcess.End, 0.25f);
                        yield break;
                    }

                    yield return null;                    
                }

                ChagngeGameMode(BattleProcess.CutIn, 0.25f);
                break;

            case BattleProcess.SecondAttack:
                //TypeMatchUpper();
                AttackAnimPos();
                while (diceNumber > 0)
                {
                    AttackAnim();
                    yield return new WaitForSeconds(0.5f);
                    if (witchAttackBool)
                    {
                        lowerCharHP--;
                        lowerText.text = lowerCharHP.ToString();
                    }
                    if (!witchAttackBool)
                    {
                        upperCharHP--;
                        upperText.text = upperCharHP.ToString();
                    }                    
                    diceNumber--;
                    yield return new WaitForSeconds(anim_Attack.GetCurrentAnimatorStateInfo(0).length);
                    if (upperCharHP == 0 || lowerCharHP == 0)
                    {
                        if (witchAttackBool)
                        {
                            lowerImage.material = grayScale;
                        }
                        if (!witchAttackBool)
                        {
                            upperImage.material = grayScale;
                        }                        
                        ChagngeGameMode(BattleProcess.End, 0.25f);
                        yield break;
                    }

                    yield return null;
                }

                ChagngeGameMode(BattleProcess.End, 0.25f);
                break;
        }
        yield break;
    }

    private void CharCutIn()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            nextPlayerTexts.gameObject.SetActive(false);
            if (doOnce)
            {
                doOnce = false;
                StartCoroutine(CutInCor(2f));
            }
        }     
        
    }

    private IEnumerator CutInCor(float waitTime)
    {
        anim_CutInMask.SetTrigger("CutInMask");
        yield return new WaitForSeconds(anim_CutInMask.GetCurrentAnimatorStateInfo(0).length + waitTime);        
        ChagngeGameMode(BattleProcess.SecondDice, 0.25f);
        yield break;
    }

    private void EndPhase()
    {
        anim_Icon.gameObject .SetActive (true);
        anim_Plate.gameObject.SetActive(true);
        anim_Text.gameObject.SetActive(true);
        anim_Attack.gameObject.SetActive(true);
        anim_FlashUpper.gameObject.SetActive(true);
        anim_FlashLower.gameObject.SetActive(true);
        anim_CutInMask.gameObject.SetActive(true);

        anim_Icon.SetBool("End", true);
        EndPass = true;
        Init();
        doOnce = false;
        P1Deck.SetActive(true);
        P2Deck.SetActive(true);
        ChagngeGameMode(BattleProcess.Start, 0.25f);

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
                    rollTheDice.gameObject.SetActive(true);
                    diceArrow.gameObject.SetActive(true);


                    beforeProcess = BattleProcess.FirstDice;
                    dice.IsDice = true;
                    diceObj.SetActive(true);
                    dice.DiceSet();

                    break;
                case BattleProcess.FirstAttack:
                    upperText.text = upperCharHP.ToString();
                    lowerText.text = lowerCharHP.ToString();
                    doOnce = true;
                    diceBool = true;                    
                    rollTheDice.gameObject.SetActive(false);
                    diceArrow.gameObject.SetActive(false);

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
                    rollTheDice.gameObject.SetActive(true);
                    diceArrow.gameObject.SetActive(true);

                    beforeProcess = BattleProcess.SecondDice;
                    dice.IsDice = true;
                    diceObj.SetActive(true);
                    dice.DiceSet();
                    break;
                case BattleProcess.SecondAttack:
                    upperText.text = upperCharHP.ToString();
                    lowerText.text = lowerCharHP.ToString();
                    doOnce = true;
                    diceBool = true;

                    //仮実装用
                    //diceText.gameObject.SetActive(false);
                    //diceButton.gameObject.SetActive(false);
                    //本実装用
                    rollTheDice.gameObject.SetActive(false);
                    diceArrow.gameObject.SetActive(false);

                    upperImage.gameObject.SetActive(true);
                    lowerImage.gameObject.SetActive(true);





                    break;
                case BattleProcess.ReRollChance:
                    doOnce = true;
                    rollTheDice.gameObject.SetActive(false);
                    diceArrow.gameObject.SetActive(false);
                    break;
                case BattleProcess.End:
                    //beta用
                    CutInImage.gameObject.SetActive(false);
                    break;


                case BattleProcess.Interval:
                    break;
            }
        }
    }
    //private void TypeMatchLower()
    //{
    //    if(lowerChar.Color == CharacterData.CharColor.Yellow && upperChar.Color == CharacterData.CharColor.Blue)
    //    {
    //        diceNumber += 1;
    //    }

    //    if (lowerChar.Color == CharacterData.CharColor.Blue && upperChar.Color == CharacterData.CharColor.Red)
    //    {
    //        diceNumber += 1;
    //    }

    //    if (lowerChar.Color == CharacterData.CharColor.Red && upperChar.Color == CharacterData.CharColor.Yellow)
    //    {
    //        diceNumber += 1;
    //    }
    //}
    //private void TypeMatchUpper()
    //{
    //    if (upperChar.Color == CharacterData.CharColor.Yellow && lowerChar.Color == CharacterData.CharColor.Blue)
    //    {
    //        diceNumber += 1;
    //    }

    //    if (upperChar.Color == CharacterData.CharColor.Blue && lowerChar.Color == CharacterData.CharColor.Red)
    //    {
    //        diceNumber += 1;
    //    }

    //    if (upperChar.Color == CharacterData.CharColor.Red && lowerChar.Color == CharacterData.CharColor.Yellow)
    //    {
    //        diceNumber += 1;
    //    }
    //}

    private void FirstShake()
    {
        if (witchAttackBool)
        {
            if (GameManager.Instance.HaveCoins_1P > 1)
            {
                //beforeProcess = BattleProcess.FirstDice;
                ChagngeGameMode(BattleProcess.ReRollChance, 0.25f);
            }
            else
            {
                ChagngeGameMode(BattleProcess.FirstAttack, 0.25f);
            }
        }
        if (!witchAttackBool)
        {
            if (GameManager.Instance.HaveCoins_2P > 1)
            {
                //beforeProcess = BattleProcess.FirstDice;
                ChagngeGameMode(BattleProcess.ReRollChance, 0.25f);
            }
            else
            {
                ChagngeGameMode(BattleProcess.FirstAttack, 0.25f);
            }
        }
    }
    private void SecondShake()
    {
        if (witchAttackBool)
        {
            if (GameManager.Instance.HaveCoins_2P > 1)
            {
                //beforeProcess = BattleProcess.SecondDice;
                ChagngeGameMode(BattleProcess.ReRollChance, 0.25f);
            }
            else
            {
                ChagngeGameMode(BattleProcess.SecondAttack, 0.25f);
            }
        }
        if (!witchAttackBool)
        {
            if (GameManager.Instance.HaveCoins_1P > 1)
            {
                //beforeProcess = BattleProcess.SecondDice;
                ChagngeGameMode(BattleProcess.ReRollChance, 0.25f);
            }
            else
            {
                ChagngeGameMode(BattleProcess.SecondAttack, 0.25f);
            }
        }
    }

    private void AttackAnimPos()
    {
        if (witchAttackBool && nowProcess == BattleProcess.FirstAttack)
        {
            anim_Attack.transform.position = upperImage.transform.position;
        }

        if (witchAttackBool && nowProcess == BattleProcess.SecondAttack)
        {
            anim_Attack.transform.position = lowerImage.transform.position;
        }


        if (!witchAttackBool && nowProcess == BattleProcess.FirstAttack)
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

        if (witchAttackBool && nowProcess == BattleProcess.SecondAttack)
        {
            anim_Attack.SetTrigger("Attack");
            anim_FlashLower.SetTrigger("FlashLower");
        }


        if (!witchAttackBool && nowProcess == BattleProcess.FirstAttack)
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

    private void other()
    {
        switch (GameManager.Instance.ChoiseChar_1P)
        {
            case 0:
                if (witchAttackBool)
                {
                    kenkaImage_Left.sprite = battleImage.characterDatas[0].IconImage;
                    kenkaImage_Right.sprite = battleImage.characterDatas[GameManager.Instance.ChoiseChar_2P].IconImage;
                    CutInImage.GetComponent<SpriteRenderer>().sprite = battleImage.characterDatas[GameManager.Instance.ChoiseChar_2P].CutInImage;
                    switch (AtkAT)
                    {
                        case CharacterManager.Attribute.Rock:
                            lowerImage.sprite = akane[0];
                            break;

                        case CharacterManager.Attribute.Paper:
                            lowerImage.sprite = akane[1];
                            break;

                        case CharacterManager.Attribute.Scissors:
                            lowerImage.sprite = akane[2];
                            break;
                    }
                }

                if (!witchAttackBool)
                {
                    kenkaImage_Left.sprite = battleImage.characterDatas[GameManager.Instance.ChoiseChar_2P].IconImage;
                    kenkaImage_Right.sprite = battleImage.characterDatas[0].IconImage;
                    CutInImage.GetComponent<SpriteRenderer>().sprite = battleImage.characterDatas[GameManager.Instance.ChoiseChar_1P].CutInImage;
                    switch (DefAT)
                    {
                        case CharacterManager.Attribute.Rock:
                            lowerImage.sprite = akane[0];
                            break;

                        case CharacterManager.Attribute.Paper:
                            lowerImage.sprite = akane[1];
                            break;

                        case CharacterManager.Attribute.Scissors:
                            lowerImage.sprite = akane[2];
                            break;
                    }
                }
                break;

            case 1:
                if (witchAttackBool)
                {
                    kenkaImage_Left.sprite = battleImage.characterDatas[1].IconImage;
                    kenkaImage_Right.sprite = battleImage.characterDatas[GameManager.Instance.ChoiseChar_2P].IconImage;
                    CutInImage.GetComponent<SpriteRenderer>().sprite = battleImage.characterDatas[GameManager.Instance.ChoiseChar_2P].CutInImage;
                    switch (AtkAT)
                    {
                        case CharacterManager.Attribute.Rock:
                            lowerImage.sprite = nana[0];
                            break;

                        case CharacterManager.Attribute.Paper:
                            lowerImage.sprite = nana[1];
                            break;

                        case CharacterManager.Attribute.Scissors:
                            lowerImage.sprite = nana[2];
                            break;
                    }
                }

                if (!witchAttackBool)
                {
                    kenkaImage_Left.sprite = battleImage.characterDatas[GameManager.Instance.ChoiseChar_2P].IconImage;
                    kenkaImage_Right.sprite = battleImage.characterDatas[1].IconImage;
                    CutInImage.GetComponent<SpriteRenderer>().sprite = battleImage.characterDatas[GameManager.Instance.ChoiseChar_1P].CutInImage;
                    switch (DefAT)
                    {
                        case CharacterManager.Attribute.Rock:
                            lowerImage.sprite = nana[0];
                            break;

                        case CharacterManager.Attribute.Paper:
                            lowerImage.sprite = nana[1];
                            break;

                        case CharacterManager.Attribute.Scissors:
                            lowerImage.sprite = nana[2];
                            break;
                    }
                }
                break;

            case 2:
                if (witchAttackBool)
                {
                    kenkaImage_Left.sprite = battleImage.characterDatas[2].IconImage;
                    kenkaImage_Right.sprite = battleImage.characterDatas[GameManager.Instance.ChoiseChar_2P].IconImage;
                    CutInImage.GetComponent<SpriteRenderer>().sprite = battleImage.characterDatas[GameManager.Instance.ChoiseChar_2P].CutInImage;
                    switch (AtkAT)
                    {
                        case CharacterManager.Attribute.Rock:
                            lowerImage.sprite = iori[0];
                            break;

                        case CharacterManager.Attribute.Paper:
                            lowerImage.sprite = iori[1];
                            break;

                        case CharacterManager.Attribute.Scissors:
                            lowerImage.sprite = iori[2];
                            break;
                    }
                }

                if (!witchAttackBool)
                {
                    kenkaImage_Left.sprite = battleImage.characterDatas[GameManager.Instance.ChoiseChar_2P].IconImage;
                    kenkaImage_Right.sprite = battleImage.characterDatas[2].IconImage;
                    CutInImage.GetComponent<SpriteRenderer>().sprite = battleImage.characterDatas[GameManager.Instance.ChoiseChar_1P].CutInImage;
                    switch (DefAT)
                    {
                        case CharacterManager.Attribute.Rock:
                            lowerImage.sprite = iori[0];
                            break;

                        case CharacterManager.Attribute.Paper:
                            lowerImage.sprite = iori[1];
                            break;

                        case CharacterManager.Attribute.Scissors:
                            lowerImage.sprite = iori[2];
                            break;
                    }
                }
                break;
        }

        switch (GameManager.Instance.ChoiseChar_2P)
        {
            case 0:
                if (witchAttackBool)
                {                    
                    switch (AtkAT)
                    {
                        case CharacterManager.Attribute.Rock:
                            upperImage.sprite = akane[0];
                            break;

                        case CharacterManager.Attribute.Paper:
                            upperImage.sprite = akane[1];
                            break;

                        case CharacterManager.Attribute.Scissors:
                            upperImage.sprite = akane[2];
                            break;
                    }
                }

                if (!witchAttackBool)
                {
                    switch (DefAT)
                    {
                        case CharacterManager.Attribute.Rock:
                            upperImage.sprite = akane[0];
                            break;

                        case CharacterManager.Attribute.Paper:
                            upperImage.sprite = akane[1];
                            break;

                        case CharacterManager.Attribute.Scissors:
                            upperImage.sprite = akane[2];
                            break;
                    }
                }
                break;
            case 1:
                if (witchAttackBool)
                {
                    switch (AtkAT)
                    {
                        case CharacterManager.Attribute.Rock:
                            upperImage.sprite = nana[0];
                            break;

                        case CharacterManager.Attribute.Paper:
                            upperImage.sprite = nana[1];
                            break;

                        case CharacterManager.Attribute.Scissors:
                            upperImage.sprite = nana[2];
                            break;
                    }
                }

                if (!witchAttackBool)
                {
                    switch (DefAT)
                    {
                        case CharacterManager.Attribute.Rock:
                            upperImage.sprite = nana[0];
                            break;

                        case CharacterManager.Attribute.Paper:
                            upperImage.sprite = nana[1];
                            break;

                        case CharacterManager.Attribute.Scissors:
                            upperImage.sprite = nana[2];
                            break;
                    }
                }
                break;
            case 2:
                if (witchAttackBool)
                {
                    switch (AtkAT)
                    {
                        case CharacterManager.Attribute.Rock:
                            upperImage.sprite = iori[0];
                            break;

                        case CharacterManager.Attribute.Paper:
                            upperImage.sprite = iori[1];
                            break;

                        case CharacterManager.Attribute.Scissors:
                            upperImage.sprite = iori[2];
                            break;
                    }
                }

                if (!witchAttackBool)
                {                    
                    switch (DefAT)
                    {
                        case CharacterManager.Attribute.Rock:
                            upperImage.sprite = iori[0];
                            break;

                        case CharacterManager.Attribute.Paper:
                            upperImage.sprite = iori[1];
                            break;

                        case CharacterManager.Attribute.Scissors:
                            upperImage.sprite = iori[2];
                            break;
                    }
                }
                break;
        }
    }
}

