using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/StageData")]
public class StageData : ScriptableObject
{
    public Vector2Int[] NG;
    public Sprite StageSprite;
    public int StageSize;
}
