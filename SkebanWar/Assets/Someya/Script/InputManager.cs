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
            if(touch.phase == TouchPhase.Began)
            {
                // 現在のタッチの座標でレイを作ります
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if(Physics.Raycast(ray))
                {
                    // ヒットしたらパーティクルを作ります
                    Instantiate(particle, transform.position, transform.rotation);
                }
            }
        }
    }
}
