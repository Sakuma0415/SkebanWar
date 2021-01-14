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

    [SerializeField]
    FieldManager fieldManager;
    [SerializeField]
    CharacterManager P1CharacterManager;
    [SerializeField]
    CharacterManager P2CharacterManager;

    [SerializeField]
    Battle battle;

    public int attack=0;
    public int defense=0;

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
                case 2:
                    Progress2();
                    break;
            }
        }
    }

    void Progress2()
    {
        Resetb();
        //character.CharacterBench[defense].HP = (character.CharacterBench[defense].HP - dice.Ans ) <= 0 ?0:( character.CharacterBench[defense].HP - dice.Ans );
        //character.CharacterBench[defense].HP -= 10;
        fieldManager.FieldClean();
        Progress.Instance.endGameMode = true;
        NextProgress();
    }

    //画面暗転＆喧嘩上等演出
    void Progress0()
    {
        float timeEnd = 0.5f;
        if (timeEnd < time)
        {
            NextProgress();
            CharacterManager character = (Progress.Instance.afterBattleTrnePlayer == 1 ? P1CharacterManager : P2CharacterManager);
            CharacterManager character2 = (Progress.Instance.afterBattleTrnePlayer == 2? P1CharacterManager : P2CharacterManager);
            battle.upperChar.HP =   character.CharacterBench[defense ].HP;
            battle.lowerChar .HP = character2.CharacterBench[attack].HP;
            Debug.Log(attack);
            battle.doOnce = true;
            //battle.witchAttackBool = Progress.Instance.afterBattleTrnePlayer == 2;
        }
    }

    void Progress1()
    {
        float timeEnd = 0.5f;

        if(!battle.EndPass)
        {
            time = 0;
        }

        if (timeEnd < time)
        {
            Debug.Log("end");
            NextProgress();
        }
    }

    public void Resetb()
    {
        CharacterManager character = (Progress.Instance.afterBattleTrnePlayer == 1 ? P1CharacterManager : P2CharacterManager);
        CharacterManager character2 = (Progress.Instance.afterBattleTrnePlayer == 2 ? P1CharacterManager : P2CharacterManager);
        character.CharacterBench[defense].HP = battle.upperChar.HP;
        character2.CharacterBench[attack].HP = battle.lowerChar.HP;
    }

}
