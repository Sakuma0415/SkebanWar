using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    [SerializeField]
    private int stageNumber;
    private bool leftSlideBool;
    private bool rightSlideBool;
    private float slideInt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slideInt >= 750)
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
            if (slideInt < 750)
            {
                transform.Translate(10f, 0, 0);
                slideInt += 10;
            }
        }

        if (leftSlideBool)
        {
            if (slideInt < 750)
            {
                transform.Translate(-10f, 0, 0);
                slideInt += 10;
            }
        }

        if (transform.position.x < -1860f)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(1500, 0, 0);
        }

        if (transform.position.x >= 2630)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1500, 0, 0);
        }
    }
}
