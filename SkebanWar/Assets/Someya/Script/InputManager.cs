using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager: MonoBehaviour
{
    GameObject particle;

    void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            // タブレットを押した時
            if(touch.phase == TouchPhase.Began)
            {
                // 現在のタッチの座標でレイを作ります
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if(Physics.Raycast(ray))
                {
                    // ヒットしたらパーティクルを作ります
                    Instantiate(particle, transform.position, transform.rotation);
                }
                Debug.Log("押した");
            }
            // タブレットを離した時
            if(touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if(Physics.Raycast(ray))
                {
                    Instantiate(particle, transform.position, transform.rotation);
                }
                Debug.Log("離した");
            }
            // タブレットを押し続いている時
            if(touch.phase == TouchPhase.Moved)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray))
                {
                    Instantiate(particle, transform.position, transform.rotation);
                }
                Debug.Log("押し続いている");
            }
        }
    }
}
