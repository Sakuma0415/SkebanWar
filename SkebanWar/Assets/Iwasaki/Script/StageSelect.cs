using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    [SerializeField]
    private int stageNumber;
    private bool leftSlideBool;
    private bool rightSlideBool;
    private float slideInt;
    private float randomTime = 0;
    private float slideTimes;
    private bool finishSlide;
    private bool finishSlideLeft;
    private bool finishSlideRight;
    private float howLongPos;
    private bool doOnce;
    private bool doOnce2 = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (doOnce2)
        {
            doOnce2 = false;
            slideTimes = RandomNumber.slideTime;
        }
        
        if (doOnce)
        {
            doOnce = false;
            StartCoroutine(WaitTime(1.5f));            
        }

        if (finishSlide)
        {
            if(howLongPos != 0)
            {
                if (howLongPos > 0)
                {
                    this.transform.Translate(-10f, 0, 0);
                    howLongPos -= 10;
                }
                else
                {
                    this.transform.Translate(10f, 0, 0);
                    howLongPos += 10;
                }
            }
            else
            {
                doOnce = true;
                finishSlide = false;
            }
        }

        if (finishSlideLeft)
        {
            if (howLongPos < -1600 || -1600 < howLongPos)
            {
                if (howLongPos > -1600)
                {
                    gameObject.transform.Translate(-10f, 0, 0);
                    howLongPos -= 10;
                }
                else
                {
                    gameObject.transform.Translate(10f, 0, 0);
                    howLongPos += 10;
                }
            }
            else
            {
                finishSlideLeft = false;
            }
        }

        if (finishSlideRight)
        {
            if (howLongPos < 1600 || 1600 < howLongPos)
            {
                if (howLongPos > 1600)
                {
                    gameObject.transform.Translate(-10f, 0, 0);
                    howLongPos -= 10;
                }
                else
                {
                    gameObject.transform.Translate(10f, 0, 0);
                    howLongPos += 10;
                }
            }
            else
            {
                finishSlideRight = false;
            }
        }

        if (randomTime > slideTimes)
        {
            leftSlideBool = false;
            if (this.gameObject.GetComponent<RectTransform>().anchoredPosition.x >= -800 && this.gameObject.GetComponent<RectTransform>().anchoredPosition.x <= 800)
            {
                finishSlide = true;
                howLongPos = this.gameObject.GetComponent<RectTransform>().anchoredPosition.x;
                GameManager.Instance.ChoiseStage = stageNumber;
            }

            if (this.gameObject.GetComponent<RectTransform>().anchoredPosition.x <= -800 && this.gameObject.GetComponent<RectTransform>().anchoredPosition.x >= -1600)
            {
                this.finishSlideLeft = true;
                this.howLongPos = this.gameObject.GetComponent<RectTransform>().anchoredPosition.x;
            }

            if (this.gameObject.GetComponent<RectTransform>().anchoredPosition.x >= 800 && this.gameObject.GetComponent<RectTransform>().anchoredPosition.x <= 1600)
            {
                this.finishSlideRight = true;
                this.howLongPos = this.gameObject.GetComponent<RectTransform>().anchoredPosition.x;
            }
        }

        if(slideInt >= 1600)
        {
            leftSlideBool = false;
            rightSlideBool = false;
            slideInt = 0;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            rightSlideBool = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            leftSlideBool = true;
        }

        if (rightSlideBool)
        {
            //自分で選べるバージョン
            //if (slideInt < 1600)
            //{
            //    transform.Translate(10f, 0, 0);
            //    slideInt += 10;
            //}
            transform.Translate(10f, 0, 0);
        }

        if (leftSlideBool)
        {
            //自分で選べるバージョン
            //if (slideInt < 1600)
            //{
            //    transform.Translate(-10f, 0, 0);
            //    slideInt += 10;
            //}
            randomTime += Time.deltaTime;
            transform.Translate(-20f, 0, 0);
        }

        if (transform.position.x < -3980f)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(3200, 0, 0);
        }

        if (transform.position.x >= 5610)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-3200, 0, 0);
        }
        
    }

    private IEnumerator WaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Action");
        yield break;
    }
}
