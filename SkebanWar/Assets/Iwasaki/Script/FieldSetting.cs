using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSetting : MonoBehaviour
{
    [SerializeField]
    private FieldManager fieldManager;
    [SerializeField]
    private StageData[] stageDatas;
    private void Awake()
    {
        switch (GameManager.Instance.ChoiseStage)
        {
            case 1:
                //kouen
                fieldManager.stageData = stageDatas[0];
                break;
            case 2:
                //school
                fieldManager.stageData = stageDatas[1];
                break;
            case 3:
                //konbini
                fieldManager.stageData = stageDatas[2];
                break;
            case 4:
                //zinzya
                fieldManager.stageData = stageDatas[3];
                break;
            case 5:
                //teibou
                fieldManager.stageData = stageDatas[4];
                break;
        }
    }
}
