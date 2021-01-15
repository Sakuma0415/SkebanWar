using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private float haveCoins_1P;
    private float haveCoins_2P;
    public float HaveCoins_1P
    {
        set
        {
            haveCoins_1P = Mathf.Clamp(value, 0, 100);
        }
        get
        {
            return haveCoins_1P;
        }
    }
    public float HaveCoins_2P
    {
        set
        {
            haveCoins_2P = Mathf.Clamp(value, 0, 100);
        }
        get
        {
            return haveCoins_2P;
        }
    }


    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }    

    void Update()
    {
        
    }
}
