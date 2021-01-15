using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SpritesDateBase/Create SpritesDateBase")]
public class SpriteData : ScriptableObject
{
    public List<Sprite> Sprites = new List<Sprite>();
}
