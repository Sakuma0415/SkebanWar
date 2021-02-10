using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : SingletonMonoBehaviour<Result>
{
    private Text redPointText;
    private Text bluePointText;
    [SerializeField]
    GameObject Text1P_A;
    [SerializeField]
    GameObject Text1P_I;
    [SerializeField]
    GameObject Text1P_N;
    [SerializeField]
    GameObject Text2P_A;
    [SerializeField]
    GameObject Text2P_I;
    [SerializeField]
    GameObject Text2P_N;
    [SerializeField]
    private GameObject blueResultText;
    [SerializeField]
    private GameObject redResultText;
    [SerializeField]
    private GameObject R_drowText;
    [SerializeField]
    private GameObject B_drowText;

    [HideInInspector]
    public int bluePoint;
    [HideInInspector]
    public int redPoint;

    private bool toTitleBool;
    [SerializeField]
    private CanvasGroup fadeCanvas;
    void Start()
    {
        redPoint = GameManager.Instance.HavePoint_1P;
        bluePoint = GameManager.Instance.HavePoint_2P;
        toTitleBool = false;
        bluePointText = GameObject.FindGameObjectWithTag("BlueText").GetComponent<Text>();
        redPointText = GameObject.FindGameObjectWithTag("RedText").GetComponent<Text>();
        StartCoroutine(Roulette());
    }

    void Update()
    {
        if(GameManager.Instance.ChoiseChar_1P == 0)
        {
            Text1P_A.SetActive(true);
        }
        else if(GameManager.Instance.ChoiseChar_1P == 2)
        {
            Text1P_I.SetActive(true);
        }
        else if(GameManager.Instance.ChoiseChar_1P == 1)
        {
            Text1P_N.SetActive(true);
        }
        if (GameManager.Instance.ChoiseChar_2P == 0)
        {
            Text2P_A.SetActive(true);
        }
        else if (GameManager.Instance.ChoiseChar_2P == 2)
        {
            Text2P_I.SetActive(true);
        }
        else if (GameManager.Instance.ChoiseChar_2P == 1)
        {
            Text2P_N.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0) && toTitleBool)
        {
            //画面をタッチしたらタイトルへ戻る
            StartCoroutine(FadeOut(1.5f));
        }
    }
    private IEnumerator Roulette()
    {
        float time = 0f;
        //whileの()内秒数間秒間ルーレットっぽく表示
        while (time < 3f)
        {
            time += Time.deltaTime;
            bluePointText.text = Random.Range(1, 30).ToString();
            redPointText.text = Random.Range(1, 30).ToString();
            yield return null;
        }        
        //最終スコアを表示
        bluePointText.text = bluePoint.ToString();
        redPointText.text = redPoint.ToString();
        //()内秒数後に勝敗の文字を表示
        StartCoroutine(WinOrLose(1f));
        yield break;
    }
    private IEnumerator WinOrLose(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(GameManager.Instance.HavePoint_2P > GameManager.Instance.HavePoint_1P)
        {
            redResultText.GetComponent<Text>().text = "負け…";
            blueResultText.GetComponent<Text>().text = "勝ち!!";
        }

        if(GameManager.Instance.HavePoint_2P < GameManager.Instance.HavePoint_1P)
        {
            redResultText.GetComponent<Text>().text = "勝ち!!";
            blueResultText.GetComponent<Text>().text = "負け…";
        }

        if(GameManager.Instance.HavePoint_2P == GameManager.Instance.HavePoint_1P)
        {
            R_drowText.GetComponent<Text>().text = "引き分け";
            R_drowText.SetActive(true);
            B_drowText.GetComponent<Text>().text = "引き分け";
            B_drowText.SetActive(true);

        }
        //勝敗のテキストを表示
        redResultText.SetActive(true);
        blueResultText.SetActive(true);
        toTitleBool = true;
        yield break;
    }
    //public void Event()
    //{
    //    if (toTitleBool)
    //    {
    //        //()内秒かけてFadeOutする
    //        StartCoroutine(FadeOut(1.5f));
    //    }
    //}
    private IEnumerator FadeOut(float fadeTime)
    {
        float time = 0f;
        while (fadeCanvas.alpha < 1)
        {
            time += Time.deltaTime;
            fadeCanvas.alpha = 1f * (time / fadeTime);
            yield return null;
        }
        SceneManager.LoadScene("Title");
        yield break;
    }
}
