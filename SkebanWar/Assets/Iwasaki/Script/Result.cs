using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    private Text redPointText;
    private Text bluePointText;
    [SerializeField]
    private GameObject blueResultText;
    [SerializeField]
    private GameObject redResultText;
    [SerializeField]
    private GameObject drowText;
    private int bluePoint = 2;
    private int redPoint = 2;

    private bool toTitleBool;
    [SerializeField]
    private CanvasGroup fadeCanvas;
    void Start()
    {
        toTitleBool = false;
        bluePointText = GameObject.FindGameObjectWithTag("BlueText").GetComponent<Text>();
        redPointText = GameObject.FindGameObjectWithTag("RedText").GetComponent<Text>();        
        StartCoroutine(Roulette());
    }

    void Update()
    {
        if(toTitleBool)
        {
            //画面をタッチしたらタイトルへ戻る                
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
        if(bluePoint > redPoint)
        {
            redResultText.GetComponent<Text>().text = "負け…";
            blueResultText.GetComponent<Text>().text = "勝ち!!";
        }

        if(bluePoint < redPoint)
        {
            redResultText.GetComponent<Text>().text = "勝ち!!";
            blueResultText.GetComponent<Text>().text = "負け…";
        }

        if(bluePoint == redPoint)
        {
            drowText.GetComponent<Text>().text = "引き分け";
            drowText.SetActive(true);
        }
        //勝敗のテキストを表示
        redResultText.SetActive(true);
        blueResultText.SetActive(true);
        toTitleBool = true;
        yield break;
    }
    public void Event()
    {
        if (toTitleBool)
        {
            //()内秒かけてFadeOutする
            StartCoroutine(FadeOut(1.5f));
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
        SceneManager.LoadScene("Title");
        yield break;
    }
}
