using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{

    [SerializeField]
    private int stageNumber;
    private bool rightSlideBool;
    private bool inputDetected = false;
    private float speed = 15.0f;
    private float slideInt;
    private int x;

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

        if(Input.GetMouseButtonDown(2))
        {
            inputDetected = false;
            if (x >= -1 && x <= 1)
            {
                this.transform.position += new Vector3(1000, 2000, 0);
            }

            if (transform.position.x >= 5000)
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            inputDetected = true;
        }

        if (inputDetected) transform.position += Vector3.right * speed;

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
}
