using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData/Create CharacterDataBase")]
public class CharDataBase : ScriptableObject
{
    public List<CharacterData> characterDatas = new List<CharacterData>();
}
