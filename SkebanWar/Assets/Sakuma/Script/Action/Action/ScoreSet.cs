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
        text.text = PlayerNum==1? fieldManager.P1Score.ToString(): fieldManager.P2Score.ToString();
    }
}
