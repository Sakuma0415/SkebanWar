using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{

    [SerializeField]
    private int stageNumber;
    private bool rightSlideBool;
    private bool inputDetected = false;
    private float speed = 15.0f;
    private float slideInt;
    private int x;
    [HideInInspector]
    public static bool TextBool;

    void start()
    {

    }

    void Update()
    {
        if (transform.position.x <= -5000)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(3000, 0, 0);
        }

        if (transform.position.x >= 5350)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-3000, 0, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (inputDetected)
            {
                TextBool = false;
                inputDetected = false;
                if (x >= -1 && x <= 1)
                {
                    this.transform.position += new Vector3(1000, 2000, 0);
                }

                if (transform.position.x >= 5000)
                {
                    gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                    GameManager.Instance.ChoiseStage = stageNumber;
                    StartCoroutine(WaitTime(1.5f));
                }
                Stagetext.goStageBool = true;
            }
        }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            if (TextBool)
            {
                inputDetected = true;
            }            
        }

        if (inputDetected)
        {
            transform.position += Vector3.right * speed;
        }

        if (slideInt >= 750)
        {
            rightSlideBool = false;
            slideInt = 0;
        }

        if (rightSlideBool)
        {
            if (slideInt < 750)
            {
                transform.Translate(10f, 0, 0);
                slideInt += 10;
            }
        }
    }
    private IEnumerator WaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Action");
        yield break;
    }
}
