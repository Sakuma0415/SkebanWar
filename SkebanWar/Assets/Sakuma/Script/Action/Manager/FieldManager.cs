using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 盤面の情報を管理するマネージャー
/// </summary>
public class FieldManager : MonoBehaviour
{
    //現在のマスの状態
    public enum MassState
    {
        None,
        Block,
        P1,
        P2,
    }

    //マスごとに保存させる情報の構造体
    public struct MassData
    {
        //マスの状態
        public MassState massState;
        public int serectSet;
        public GameObject MassPre;

        public void Init()
        {
            massState = MassState.None;
            serectSet = 0;
        }
    }

    //ステージの状態
    public MassData[,] massDatas;

    //ステージのサイズ
    public int stageSize = 0;

    //テストの座標設定
    [SerializeField]
    Vector2Int  testSet=Vector2Int.zero ;

    //ステージの表示範囲
    public Vector2 fieldSpace = Vector2.zero;

    [SerializeField]
    GameObject massPrefab;

    
    //開始時処理
    private void Start()
    {
        FieldInit();
    }

    //盤面の初期化処理
    void FieldInit()
    {
        massDatas = new MassData[stageSize, stageSize];
        for(int i=0;i< stageSize; i++)
        {
            for(int j = 0; j < stageSize; j++)
            {
                massDatas[i, j].Init();
                GameObject MassObj= Instantiate(massPrefab);
                Vector3 size = new Vector3(fieldSpace.x / stageSize, fieldSpace.y / stageSize);
                MassObj.transform.localScale = size * MassObj.transform.localScale.x;
                MassObj.transform.position = transform.position + new Vector3(size.x * i, -size.y * j)-new Vector3(size.x*((stageSize / 2) -(stageSize%2==0? 0.5f:0)), -1* size.y * ((stageSize / 2)- (stageSize % 2 == 0 ? 0.5f : 0)),0) ;
                MassObj.transform.parent = transform;
                massDatas[i, j].MassPre = MassObj;
            }
        }
    }


    //フレーム処理
    private void Update()
    {
        ////テスト用
        if(Input.GetKeyDown (KeyCode.A ))
        {
            VisualUpdate();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MassSet(1, testSet.x, testSet.y);
        }
    }

    //マスに情報をセット
    public void MassSet(int playerNum,int X,int Y)
    {
        if(X<0||X>=stageSize && Y < 0 || Y >= stageSize)
        {
            Debug.Log("設定する情報が範囲外です");
            return;
        }
        switch (playerNum)
        {
            case 0:
                massDatas[X, Y].massState = MassState.Block;
                break;
            case 1:
                massDatas[X, Y].massState = MassState.P1;
                break;
            case 2:
                massDatas[X, Y].massState = MassState.P2;
                break;
            default:
                Debug.Log("マスの情報更新に失敗しました");
                return;
        }
        VisualUpdate();
    }




    /////////////////////
    //演出上の関数
    /////////////////////


    public void VisualUpdate()
    {
        for (int i = 0; i < stageSize; i++)
        {
            for (int j = 0; j < stageSize; j++)
            {
                switch (massDatas[j, i].massState)
                {
                    case MassState.None:
                        massDatas[j, i].MassPre.GetComponent<SpriteRenderer>().color =Color.white ;
                        break;
                    case MassState.P1:
                        massDatas[j, i].MassPre.GetComponent<SpriteRenderer>().color = Color.red ;
                        break;
                    case MassState.P2:
                        massDatas[j, i].MassPre.GetComponent<SpriteRenderer>().color = Color.blue ;
                        break;
                    case MassState.Block:
                        massDatas[j, i].MassPre.GetComponent<SpriteRenderer>().color = Color.black;
                        break;
                }
                
            }
        }
    }



    //ギズモ
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red ;
        Gizmos.DrawWireCube(transform.position ,new Vector3 (fieldSpace .x, fieldSpace.y,0));
    }

}
