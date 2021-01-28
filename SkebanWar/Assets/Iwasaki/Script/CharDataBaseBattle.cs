using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "CharDataInBattle/Create CharDataBaseBattle")]
public class CharDataBaseBattle : ScriptableObject
{
    public List<CharDataInBattle> characterDatas = new List<CharDataInBattle>();
}
