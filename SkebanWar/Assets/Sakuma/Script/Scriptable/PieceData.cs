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
    public bool[,] piecePos = new bool[3, 3];


    [CustomEditor(typeof(PieceData))]
    public class CharacterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            PieceData chara = target as PieceData;

            chara.name = EditorGUILayout.TextField("名前", chara.name);
            //chara.piecePos = new bool[chara.pieceLeng, chara.pieceLeng];


            EditorGUILayout.LabelField("ピースの配置");
            EditorGUILayout.LabelField("※赤のトグルが回転の中心です。");
            for (int i = 0; i < 3; i++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int j=0;j< 3; j++)
                {
                    if (i == 1 && j == 1)
                    {
                        GUI.color = Color.red;
                    }
                    else
                    {
                        GUI.color = Color.white;
                    }
                    chara.piecePos[j, i] = EditorGUILayout.Toggle("", chara.piecePos[j, i], GUILayout.Width(12));
                }

                EditorGUILayout.EndHorizontal();
            }



        }
    }




}
