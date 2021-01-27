using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private float haveCoins_1P;
    private float haveCoins_2P;
    private int choiseChar_1P;
    private int choiseChar_2P;
    [HideInInspector]
    public static int havePoint_1P;
    [HideInInspector]
    public static int havePoint_2P;
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
    public int ChoiseChar_1P
    {
        set
        {
            choiseChar_1P = Mathf.Clamp(value, 0, 3);
        }
        get
        {
            return choiseChar_1P;
        }
    }
    public int ChoiseChar_2P
    {
        set
        {
            choiseChar_2P = Mathf.Clamp(value, 0, 3);
        }
        get
        {
            return choiseChar_2P;
        }
    }
    public int HavePoint_1P
    {
        set
        {
            havePoint_1P = Mathf.Clamp(value, 0, 100);
        }
        get
        {
            return havePoint_1P;
        }
    }
    public int HavePoint_2P
    {
        set
        {
            havePoint_2P = Mathf.Clamp(value, 0, 100);
        }
        get
        {
            return havePoint_2P;
        }
    }
    public bool order;


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
