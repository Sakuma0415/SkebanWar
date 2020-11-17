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

    //配置するピースの形
    public PieceData pieceData;

    //回転の状態
    public int rotState = 0;

    //配置予告のオブジェクトのプレハブ
    [SerializeField]
    GameObject noticeObj;

    //配置予告の配列
    GameObject[] notice= new GameObject[9];

    //予告の色
    [SerializeField ]
    SpriteRenderer[] noticeColor = new SpriteRenderer[9]; 

    void Start()
    {
        for(int i = 0; i < 9;i++)
        {
            notice[i] = Instantiate(noticeObj);
            notice[i].SetActive(false);
            notice[i].transform.parent = transform;
            noticeColor[i]=notice[i].GetComponent<SpriteRenderer>();
        }
    }


    void Update()
    {
        if (IsSelect)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {

                if (MassCheck())
                {
                    for (int i = 0; i < pieceData.posSet.Length; i++)
                    {
                        Vector2Int pos = MousePosInField()+ pieceData.posSet[i];
                        fieldManager.MassSet(2, pos.x, pos.y);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (MassCheck())
                {
                    for (int i = 0; i < pieceData.posSet.Length; i++)
                    {
                        Vector2Int pos = MousePosInField() + pieceData.posSet[i];
                        fieldManager.MassSet(1, pos.x, pos.y);
                    }
                }
            }
            Vector2Int check = MousePosInField();
            if (check != new Vector2Int(-1, -1))
            {
                
                for (int i = 0; i <9 ; i++)
                {
                    if(i< pieceData.posSet.Length)
                    {
                        notice[i].SetActive(true);
                        notice[i].transform.position = fieldManager.massDatas[check.x, check.y].MassPre.transform.position+ new Vector3(pieceData.posSet[i].x , -pieceData.posSet[i].y);

                        if (MassCheck())
                        {
                            noticeColor[i].color  = Color.green;
                        }
                        else
                        {
                            noticeColor[i].color  = Color.red;
                        }
                    }
                    else
                    {
                        notice[i].SetActive(false);
                    }
                }



            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                     notice[i].SetActive(false);
                    
                }
            }







        }
    }

    //ピースが盤面外に出ないかどうかのチェック
    private bool MassCheck()
    {
        Vector2Int check = MousePosInField();
        bool massCheck = true;
        if(check==new Vector2Int(-1, -1))
        {
            massCheck = false;
        }
        for(int i = 0; i < pieceData.posSet.Length ; i++)
        {
            int data = pieceData.posSet[i].x + check.x;
            int data2 = pieceData.posSet[i].y + check.y;

            if(data<0||data>= fieldManager.stageSize|| data2 < 0 || data2 >= fieldManager.stageSize)
            {
                massCheck = false;
            }
        }

        Debug.Log(massCheck);

        return massCheck;
    }



    //盤面上のマウス座標を配列の番号に変換
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
