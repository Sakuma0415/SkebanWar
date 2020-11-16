using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タップに関する処理をまとめるクラス
/// </summary>
public class TouchManager : MonoBehaviour
{
    //FieldManager
    [SerializeField]
    FieldManager fieldManager;

    //マスの選択できる状態かどうかのフラグ
    [SerializeField]
    bool IsSelect = false;

    void Start()
    {
        
    }


    void Update()
    {
        if (IsSelect)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector2Int check = MousePosInField();
                if (check != new Vector2Int(-1, -1))
                {
                    fieldManager.MassSet(1, check.x, check.y);
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Vector2Int check = MousePosInField();
                if (check != new Vector2Int(-1, -1))
                {
                    fieldManager.MassSet(2, check.x, check.y);
                }
            }

        }
    }

    public Vector2Int MousePosInField()
    {

        //フィールド左上の座標
        Vector2 pivotPos = (Vector2)fieldManager.gameObject.transform.position + new Vector2(-fieldManager.fieldSpace.x / 2, fieldManager.fieldSpace.y / 2);
        //マスの大きさ
        Vector2 MassSize = new Vector2(fieldManager.fieldSpace.x / fieldManager.stageSize, fieldManager.fieldSpace.y / fieldManager.stageSize);
        //マウスの座標
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x > pivotPos.x && mousePos.y < pivotPos.y && mousePos.x < pivotPos.x + fieldManager.fieldSpace.x && mousePos.y > pivotPos.y - fieldManager.fieldSpace.y)
        {
            Vector2Int lengPos = new Vector2Int(0, 0);

            for (int i = 0; i < fieldManager.stageSize; i++)
            {
                if (mousePos.x < pivotPos.x + ((i + 1) * MassSize.x))
                {
                    lengPos.x = i;
                    break;
                }
            }

            for (int i = 0; i < fieldManager.stageSize; i++)
            {
                if (mousePos.y > pivotPos.y - ((i + 1) * MassSize.y))
                {
                    lengPos.y = i;
                    break;
                }
            }

            return lengPos;
        }
        else
        {
            return new Vector2Int (-1,-1);
        }

    }


}
