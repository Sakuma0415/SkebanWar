using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleManager : MonoBehaviour
{
    public bool IsBattle = false;
    int battleProgress = 0;
    float time = 0;

    //暗転幕
    [SerializeField]
    Image Darkening;
    //
    [SerializeField]
    float Darkeninglate = 0;

    [SerializeField]
    Dice dice;


    [SerializeField]
    GameObject diceObj;

    public void BattleStart()
    {
        IsBattle = true;
        battleProgress = 0;
        Darkening.gameObject.SetActive(true);
        Darkening.color = new Color(0, 0, 0, 0);
    }

    void NextProgress()
    {
        battleProgress++;
        time = 0;
    }

    void Update()
    {
        if (IsBattle)
        {
            time += Time.deltaTime;
            switch (battleProgress)
            {
                case 0:
                    Progress0();
                    break;
                case 1:
                    Progress1();
                    break;
            }
        }
    }
    //画面暗転＆喧嘩上等演出
    void Progress0()
    {
        float timeEnd = 0.5f;
        if (timeEnd < time)
        {
            NextProgress();
            dice.IsDice = true;
            diceObj.SetActive(true);
            dice.DiceSet();
        }
        else
        {
            Darkening.color =new Color (0,0,0, ((time / timeEnd) * Darkeninglate)/255);
        }
    }

    void Progress1()
    {
        float timeEnd = 0.5f;

        if(!dice.diceEnd)
        {
            time = 0;
        }

        if (timeEnd < time)
        {
            NextProgress();
            dice.IsDice = false;
            diceObj.SetActive(false);
            Darkening.gameObject.SetActive(false);
            dice.DiceSet();
        }
    }



}
