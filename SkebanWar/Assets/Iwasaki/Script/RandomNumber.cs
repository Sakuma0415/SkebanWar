using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumber : MonoBehaviour
{
    [HideInInspector]
    public static float slideTime;
    // Start is called before the first frame update
    void Start()
    {
        slideTime = Random.Range(2.5f, 6.0f);
    }
}
