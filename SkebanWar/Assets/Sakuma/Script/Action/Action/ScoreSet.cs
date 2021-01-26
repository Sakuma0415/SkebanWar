using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreSet : MonoBehaviour
{
    [SerializeField]
    FieldManager fieldManager;
    [SerializeField]
    int PlayerNum = 1;
    [SerializeField]
    Text text;
    void Update()
    {
        text.text = PlayerNum == 1 ? fieldManager.P1Score.ToString(): fieldManager.P2Score.ToString();
        //if(PlayerNum == 1)
        //{
        //    GameManager.Instance.HavePoint_1P = fieldManager.P1Score;
        //}

        //if (PlayerNum == 2)
        //{
        //    GameManager.Instance.HavePoint_2P = fieldManager.P2Score;
        //}
    }
}
