using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public enum Attribute
    {
        Rock,
        Paper,
        Scissors,
        None
    }

    [System .Serializable ]
    public struct CharacterStatus
    {
        public string name;
        public Attribute attribute;
        public int HP;
        public bool IsStand;
        public void Init()
        {
            name = "";
            HP = -1;
            attribute = Attribute.None;
            IsStand = false;
        }
    }

    public CharacterStatus[] CharacterBench;
    public int count = 0;
    public int getChar = 0;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        count = 0;
        getChar = 0;
        CharacterBench = new CharacterStatus[6];

        for(int i=0;i< CharacterBench.Length; i++)
        {
            CharacterBench[i].Init();
        }

    }

    public void BenchSet(PieceData pieceData)
    {
        CharacterBench[count].name = pieceData.name;
        CharacterBench[count].HP = pieceData.HP;
        CharacterBench[count].IsStand = true;

        switch (pieceData.element)
        {
            case PieceData.Element.P:
                CharacterBench[count].attribute = Attribute.Paper;
                break;
            case PieceData.Element.R:
                CharacterBench[count].attribute = Attribute.Rock;
                break;
            case PieceData.Element.S:
                CharacterBench[count].attribute = Attribute.Scissors;
                break;
        }



        count++;
    }

    void Update()
    {
        
    }
}
