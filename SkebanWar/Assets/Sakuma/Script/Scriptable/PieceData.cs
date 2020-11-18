using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 配置する形を保存するオブジェクトクラス
/// </summary>
[CreateAssetMenu(menuName = "Scriptable/PieceData")]
public class PieceData : ScriptableObject
{

    //名前
    public string name;

    //ピースの場所
    public Vector2Int[] posSet;


    //トグル
    public bool T0;
    public bool T1;
    public bool T2;
    public bool T3;
    public bool T4;
    public bool T5;
    public bool T6;
    public bool T7;
    public bool T8;

    public bool[,] piecePos
    {
        get { return new bool[,] { { T0, T1, T2 }, { T3, T4, T5 }, { T6, T7, T8 } }; }
        private set { this.piecePos = value; }
    }


    [CustomEditor (typeof (PieceData ))]
    public class PieceEditor:Editor 
    {
        public override void OnInspectorGUI()
        {

            PieceData pieceData = target as PieceData;

            pieceData.name = EditorGUILayout.TextField("名前", pieceData.name);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("配置");
            EditorGUILayout.LabelField("赤のトグルが回転の中心です");
            //
            EditorGUILayout.BeginHorizontal();

            GUI.color = Color.white;
            pieceData.T0 = EditorGUILayout.Toggle("", pieceData.T0, GUILayout.Width(12));
            pieceData.T1 = EditorGUILayout.Toggle("", pieceData.T1, GUILayout.Width(12));
            pieceData.T2 = EditorGUILayout.Toggle("", pieceData.T2, GUILayout.Width(12));

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            pieceData.T3 = EditorGUILayout.Toggle("", pieceData.T3, GUILayout.Width(12));
            GUI.color = Color.red ;
            pieceData.T4 = EditorGUILayout.Toggle("", pieceData.T4, GUILayout.Width(12));
            GUI.color = Color.white;
            pieceData.T5 = EditorGUILayout.Toggle("", pieceData.T5, GUILayout.Width(12));

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            pieceData.T6 = EditorGUILayout.Toggle("", pieceData.T6, GUILayout.Width(12));
            pieceData.T7 = EditorGUILayout.Toggle("", pieceData.T7, GUILayout.Width(12));
            pieceData.T8 = EditorGUILayout.Toggle("", pieceData.T8, GUILayout.Width(12));

            EditorGUILayout.EndHorizontal();



        }



    }



}


