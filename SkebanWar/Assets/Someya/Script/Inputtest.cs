using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputtest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Debug.Log("押した");
            }

            if(touch.phase == TouchPhase.Ended)
            {
                Debug.Log("離した");
            }
        }
    }
}
