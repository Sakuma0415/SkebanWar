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


}
