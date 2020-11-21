using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable/DeckData")]
public class DeckData : ScriptableObject
{
    public string name;
    public PieceData[] pieceDatas;
}
