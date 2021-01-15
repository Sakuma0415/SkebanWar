using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "CharacterData/Create CharacterData")]
public class CharacterData : ScriptableObject
{
    public Sprite Image;
    public enum CharColor
    {
        Yellow,
        Red,
        Blue,
    }
    
    public CharColor Color = CharColor.Yellow;

    public int HP;

    public Sprite IconImage;

    public Sprite CutInImage;
}
