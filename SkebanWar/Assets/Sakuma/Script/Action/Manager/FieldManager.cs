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

    public  struct MassOverlap
    {
        public int PlayerNum;
        public int BenchNum;
        public void Init()
        {
            PlayerNum = 0;
            BenchNum = -1;
        }
    }

    //マスごとに保存させる情報の構造体
    public struct MassData
    {
        //マスの状態
        public MassState massState;
        public GameObject MassPre;
        public MassOverlap[] Overlap;
        public PcSelect pcSelect;
        public void Init()
        {
            massState = MassState.None;
            Overlap= new MassOverlap[1];
            Overlap[0].Init();
        }
    }

    //ステージの状態
    public MassData[,] massDatas;

    //ステージのサイズ
    public int stageSize = 0;

    //テストの座標設定
    //[SerializeField]
    //Vector2Int  testSet=Vector2Int.zero ;

    //ステージの表示範囲
    public Vector2 fieldSpace = Vector2.zero;

    [SerializeField]
    GameObject massPrefab;


    //現在の得点
    public int P1Score = 0;
    public int P2Score = 0;

    //P1のCharacterManager
    [SerializeField]
    CharacterManager P1CharacterManager;

    //P2のCharacterManager
    [SerializeField]
    CharacterManager P2CharacterManager;

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
                massDatas[i, j].pcSelect = MassObj.GetComponent<PcSelect>();
                massDatas[i, j].MassPre = MassObj;
            }
        }
    }


    //フレーム処理
    private void Update()
    {
        ////テスト用
        //if(Input.GetKeyDown (KeyCode.A ))
        //{
        //    VisualUpdate();
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    MassSet(1, testSet.x, testSet.y);
        //}
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
                OverlapSet(playerNum, X, Y);
                massDatas[X, Y].massState = MassState.P1;
                massDatas[X, Y].Overlap[massDatas[X, Y].Overlap.Length - 1].BenchNum = P1CharacterManager.count;
                break;
            case 2:
                OverlapSet(playerNum, X, Y);
                massDatas[X, Y].massState = MassState.P2;
                massDatas[X, Y].Overlap[massDatas[X, Y].Overlap.Length - 1].BenchNum = P2CharacterManager.count;
                break;
            default:
                Debug.Log("マスの情報更新に失敗しました");
                return;
        }
        VisualUpdate();
    }

    private void OverlapSet(int playerNum,int X, int Y)
    {
        if(massDatas[X, Y].Overlap[0].PlayerNum  == 0)
        {
            massDatas[X, Y].Overlap[0].PlayerNum = playerNum;
        }
        else
        {
            System.Array.Resize(ref massDatas[X, Y].Overlap, massDatas[X, Y].Overlap.Length +1);
            massDatas[X, Y].Overlap[massDatas[X, Y].Overlap.Length -1].PlayerNum = playerNum;
        }




    }

    public void ScoreSet()
    {
        int P1count = 0;
        int P2count = 0;

        for (int i = 0; i < stageSize; i++)
        {
            for (int j = 0; j < stageSize; j++)
            {
                if(massDatas[i, j].massState == MassState.P1)
                {
                    P1count++;
                }
                if (massDatas[i, j].massState == MassState.P2)
                {
                    P2count++;
                }
            }
        }

        P1Score = P1count;
        P2Score = P2count;

    }

    //攻撃対象を選択するときの画面暗転

    public void AttackSelect(int[] Attack,int trne)
    {

        for (int i = 0; i < stageSize; i++)
        {
            for (int j = 0; j < stageSize; j++)
            {
                bool select = false;
                for(int a = 0; a < massDatas[i, j].Overlap.Length; a++)
                {
                    for(int b=0;b< Attack.Length; b++)
                    {
                        if(massDatas[i, j].Overlap[a].BenchNum == Attack[b]&& trne== massDatas[i, j].Overlap[a].PlayerNum )
                        {
                            select = true;
                        }
                    }
                }
                massDatas[i, j].pcSelect .attackSelectMode  = select?1:2;

            }
        }


    }

    public void AttackSelectEnd()
    {
        for (int i = 0; i < stageSize; i++)
        {
            for (int j = 0; j < stageSize; j++)
            {
                massDatas[j, i].pcSelect.attackSelectMode = 0;

            }
        }
        Debug.Log("d");
    }




    //HPが0になっていてかつ相手の駒が重なっている場合の削除処理
    public void FieldClean()
    {

        //P1の整理
        for(int a=0; a<P1CharacterManager.CharacterBench.Length;a++)
        {
            if (a == -1)
            {
                break;
            }

            if(P1CharacterManager.CharacterBench[a].HP == 0)
            {
                for (int i = 0; i < stageSize; i++)
                {
                    for (int j = 0; j < stageSize; j++)
                    {
                        int CleanSet = 0;
                        int leng = massDatas[i, j].Overlap.Length;
                        MassOverlap [] count = new MassOverlap[1];
                        count[0].Init();
                        for (int ii=0;ii< massDatas[i, j].Overlap.Length; ii++)
                        {
                            if(massDatas[i, j].Overlap[ii].PlayerNum == 2 || massDatas[i, j].Overlap[ii].BenchNum != a)
                            {
                                if (CleanSet != 0)
                                {
                                    System.Array.Resize(ref count, count.Length  + 1);
                                }
                                count[CleanSet].BenchNum = massDatas[i, j].Overlap[ii].BenchNum;
                                count[CleanSet].PlayerNum  = massDatas[i, j].Overlap[ii].PlayerNum;

                                CleanSet++;
                            }
                        }

                        if (CleanSet>0)
                        {
                            P1CharacterManager.getChar += leng - CleanSet;
                            massDatas[i, j].Overlap = count;
                            switch (massDatas[i, j].Overlap[massDatas[i, j].Overlap.Length -1].PlayerNum)
                            {
                                case 1:
                                    massDatas[i, j].massState = MassState.P1;
                                    break;
                                case 2:
                                    massDatas[i, j].massState = MassState.P2;
                                    break;
                            }
                            
                        }

                    }
                }
            }
        }

        //P2の整理
        for (int a = 0; a < P2CharacterManager.CharacterBench.Length; a++)
        {
            if (a == -1)
            {
                break;
            }

            if (P2CharacterManager.CharacterBench[a].HP == 0)
            {
                for (int i = 0; i < stageSize; i++)
                {
                    for (int j = 0; j < stageSize; j++)
                    {
                        int CleanSet = 0;
                        int leng = massDatas[i, j].Overlap.Length;
                        MassOverlap[] count = new MassOverlap[0];
                        count[1].Init();
                        for (int ii = 0; ii < massDatas[i, j].Overlap.Length; ii++)
                        {
                            if (massDatas[i, j].Overlap[ii].PlayerNum == 1 || massDatas[i, j].Overlap[ii].BenchNum != a)
                            {
                                if (CleanSet != 0)
                                {
                                    System.Array.Resize(ref count, count.Length + 1);
                                }
                                count[CleanSet].BenchNum = massDatas[i, j].Overlap[ii].BenchNum;
                                count[CleanSet].PlayerNum = massDatas[i, j].Overlap[ii].PlayerNum;

                                CleanSet++;
                            }
                        }

                        if (CleanSet > 0)
                        {
                            P2CharacterManager.getChar += leng - CleanSet;
                            massDatas[i, j].Overlap = count;
                            switch (massDatas[i, j].Overlap[massDatas[i, j].Overlap.Length - 1].PlayerNum)
                            {
                                case 1:
                                    massDatas[i, j].massState = MassState.P1;
                                    break;
                                case 2:
                                    massDatas[i, j].massState = MassState.P2;
                                    break;
                            }
                        }

                    }
                }
            }
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
                massDatas[j, i].pcSelect.massOverlaps = massDatas[j, i].Overlap;
                //switch (massDatas[j, i].massState)
                //{
                //    case MassState.None:
                //        massDatas[j, i].MassPre.GetComponent<PcSelect>().mode = 0;
                //        break;
                //    case MassState.P1:
                //        massDatas[j, i].MassPre.GetComponent<PcSelect>().mode =1;
                //        break;
                //    case MassState.P2:
                //        massDatas[j, i].MassPre.GetComponent<PcSelect>().mode = 2;
                //        break;
                //    case MassState.Block:
                //        massDatas[j, i].MassPre.GetComponent<SpriteRenderer>().color = Color.black;
                //        break;
                //}
                
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
