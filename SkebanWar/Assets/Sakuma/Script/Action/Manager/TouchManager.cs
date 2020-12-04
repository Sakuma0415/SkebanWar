﻿using System.Collections;
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

    //マスの比
    Vector2 MassScale = Vector2 .zero ;

    //マスのトグルを座標に変換するためのバッファ
    Vector2Int[] MassPos;

    //キャラクターをホールドしているかどうかのフラグ
    //bool IsHold = false;

    //
    [SerializeField]
    int holdProgress = 0;

    //P1の手持ち
    [SerializeField]
    OnHand[] P1onHands;

    //P2の手持ち
    [SerializeField]
    OnHand[] P2onHands;

    //手持ちのScale
    [SerializeField]
    float onHandS = 0;

    //Holdした配列番号
    int holdCont = -1;

    //P1のCharacterManager
    [SerializeField]
    CharacterManager P1CharacterManager;

    //P2のCharacterManager
    [SerializeField]
    CharacterManager P2CharacterManager;

    Vector2 MsPosBf = Vector2.zero;

    [SerializeField]
    GameObject touchPro;

    void Start()
    {
        for(int i = 0; i < 9;i++)
        {
            
            notice[i] = Instantiate(noticeObj);
            notice[i].SetActive(false);
            MassScale = fieldManager.fieldSpace / fieldManager.stageSize ;
            notice[i].transform.localScale = MassScale * notice[i].transform.localScale;
            notice[i].transform.parent = transform;
            noticeColor[i]=notice[i].GetComponent<SpriteRenderer>();
        }


        ToggleToPos();
    }


    void Update()
    {
        IsSelect = Progress.Instance.gameMode == Progress.GameMode.P1Select || Progress.Instance.gameMode == Progress.GameMode.P2Select;

        touchPro.SetActive(holdProgress == 2);



        if (IsSelect)
        {

            if (Input.GetKeyDown(KeyCode.S))
            {
                Vector2Int test = MousePosInField();
                
                for(int i=0;i< fieldManager.massDatas[test.x, test.y].Overlap.Length; i++)
                {
                    Debug.Log(fieldManager.massDatas[test.x, test.y].Overlap[i].BenchNum + " _ "+ fieldManager.massDatas[test.x, test.y].Overlap[i].PlayerNum );
                }
                
            }

            //最初のクリック
            if (Input.GetKeyDown(KeyCode.Mouse0) && holdProgress == 0)
            {
                OnHand[] onHands= Progress.Instance.gameMode == Progress.GameMode.P1Select? P1onHands : P2onHands;
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                for (int i=0;i< onHands.Length; i++)
                {
                    if(onHands[i].pieceData !=null&&
                        onHands[i].gameObject .transform .position .x + onHandS > mousePos.x &&
                        onHands[i].gameObject.transform.position.x - onHandS < mousePos.x &&
                        onHands[i].gameObject.transform.position.y + onHandS > mousePos.y &&
                        onHands[i].gameObject.transform.position.y - onHandS < mousePos.y
                        )
                    {
                        //Debug.Log(onHands[i].pieceData.name);
                        pieceData = onHands[i].pieceData;
                        holdProgress = 1;
                        ToggleToPos();
                        holdCont = i;
                        break;
                    }
                }
            }

            //最初のクリック
            if (Input.GetKeyDown(KeyCode.Mouse0) && holdProgress == 2)
            {
                Vector2Int inField = MousePosInField();
                bool Catch = false;

                for (int i = 0; i < MassPos.Length; i++)
                {
                    Vector2Int pos = MousePosInField(true, MsPosBf) + MassPos[i];
                    if (inField == pos)
                    {
                        Catch = true;
                        break;
                    }
                }

                if (Catch)
                {
                    holdProgress = 1;
                }

            }






            if (Input.GetKeyUp(KeyCode.Mouse0)&& holdProgress==1)
            {
                Vector2 pivotPos = (Vector2)fieldManager.gameObject.transform.position + new Vector2(-fieldManager.fieldSpace.x / 2, fieldManager.fieldSpace.y / 2);
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (mousePos.x > pivotPos.x && mousePos.y < pivotPos.y && mousePos.x < pivotPos.x + fieldManager.fieldSpace.x && mousePos.y > pivotPos.y - fieldManager.fieldSpace.y)
                {
                    holdProgress = 2;
                    MsPosBf = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //touchPro.transform.position = MsPosBf+new Vector2 (0.5f,-0.5f);
                }
                else
                {
                    holdProgress = 0;
                }
            }

            //if (Input.GetKeyDown(KeyCode.Mouse1))
            //{

            //}

        }


        //配置予告の表示
        Vector2Int check = MousePosInField();
        if (holdProgress == 2)
        {
            check = MousePosInField(true, MsPosBf);
        }
        if (check != new Vector2Int(-1, -1) && IsSelect&& (holdProgress==1||holdProgress == 2))
        {


            for (int i = 0; i < 9; i++)
            {
                if (i < MassPos.Length)
                {
                    notice[i].SetActive(true);
                    notice[i].transform.position = fieldManager.massDatas[check.x, check.y].MassPre.transform.position + new Vector3(MassPos[i].x * MassScale.x, -MassPos[i].y * MassScale.y);

                    if (holdProgress == 2?MassCheck(true, MsPosBf): MassCheck())
                    {
                        noticeColor[i].color = Color.green;
                    }
                    else
                    {
                        noticeColor[i].color = Color.red;
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

    //ピースが盤面外に出ないかどうかのチェック
    private bool MassCheck(bool IS = false, Vector2 vec2 = new Vector2())
    {
        Vector2Int check = MousePosInField(IS, vec2);
        bool massCheck = true;
        if(check==new Vector2Int(-1, -1))
        {
            massCheck = false;
        }
        for(int i = 0; i < MassPos.Length ; i++)
        {
            int data = MassPos[i].x + check.x;
            int data2 = MassPos[i].y + check.y;

            if(data<0||data>= fieldManager.stageSize|| data2 < 0 || data2 >= fieldManager.stageSize)
            {
                massCheck = false;
            }
        }

        return massCheck;
    }



    //盤面上のマウス座標を配列の番号に変換
    public Vector2Int MousePosInField(bool IS=false ,Vector2 vec2=new Vector2() )
    {

        //フィールド左上の座標
        Vector2 pivotPos = (Vector2)fieldManager.gameObject.transform.position + new Vector2(-fieldManager.fieldSpace.x / 2, fieldManager.fieldSpace.y / 2);
        //マスの大きさ
        Vector2 MassSize = new Vector2(fieldManager.fieldSpace.x / fieldManager.stageSize, fieldManager.fieldSpace.y / fieldManager.stageSize);
        //マウスの座標
        Vector2 mousePos = Vector2.zero;
        if (IS)
        {

            mousePos = vec2;
        }
        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

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



    //トグルから座標に変換
    private void ToggleToPos()
    {

        int count = 0;

        bool[,] piecePos = {
            { pieceData .T0 , pieceData.T1 , pieceData.T2 } ,
            { pieceData .T3 , pieceData.T4 , pieceData.T5 } ,
            { pieceData .T6 , pieceData.T7 , pieceData.T8 } ,
        };

        //trueの数を検索
        for (int i=0; i<3;i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(piecePos[j, i])
                {
                    count++;
                }
            }
        }

        MassPos = new Vector2Int[count];
        count = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (piecePos[j, i])
                {
                    switch (rotState)
                    {
                        case 0:
                            MassPos[count] = new Vector2Int(i - 1, j - 1);
                            break;
                        case 1:
                            MassPos[count] = new Vector2Int(-j + 1, i - 1);
                            break;
                        case 2:
                            MassPos[count] = new Vector2Int(-i + 1, -j + 1);
                            break;
                        case 3:
                            MassPos[count] = new Vector2Int(j - 1, -i + 1);
                            break;
                    }
                    

                    count++;
                }
            }
        }

    }

    public void PUT()
    {

        if (MassCheck(true, MsPosBf))
        {
            if (holdProgress == 2)
            {
                for (int i = 0; i < MassPos.Length; i++)
                {
                    Vector2Int pos = MousePosInField(true, MsPosBf) + MassPos[i];
                    fieldManager.MassSet(Progress.Instance.gameMode == Progress.GameMode.P1Select ? 1 : 2, pos.x, pos.y);
                    Progress.Instance.endGameMode = true;
                }

                CharacterManager characterManager = Progress.Instance.gameMode == Progress.GameMode.P1Select ? P1CharacterManager : P2CharacterManager;
                characterManager.BenchSet(pieceData);

                OnHand[] onHands = Progress.Instance.gameMode == Progress.GameMode.P1Select ? P1onHands : P2onHands;
                onHands[holdCont].pieceData = null;
                fieldManager.ScoreSet();
                //仮につきのち削除
                fieldManager.FieldClean();
                holdProgress = 0;
            }
        }


    }


    public void ROT()
    {
        rotState = rotState == 3 ? 0 : rotState + 1;
        ToggleToPos();
    }

    public void CANT()
    {
        holdProgress = 0;
    }

}
