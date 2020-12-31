using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public DeckData P1deckData;

    public int[] deckSet;

    //P1の手持ち
    [SerializeField]
    OnHand[] P1onHands;

    public int P1cont = 0;


    void Start()
    {
        int[] data = new int[] {0,1,2,3,4,5 };
        deckSet = data.OrderBy(i => Guid.NewGuid()).ToArray();

        HandSet();
    }


    void HandSet()
    {
        for (int i=0;i< P1onHands.Length ;i++)
        {
            P1onHands[i].pieceData = P1deckData.pieceDatas[deckSet[i]];
        }

        P1cont = 3;
    }

    public void Draw()
    {
        if (P1cont != 6)
        {
            for (int i = 0; i < P1onHands.Length; i++)
            {
                if (P1onHands[i].pieceData == null)
                {
                    P1onHands[i].pieceData = P1deckData.pieceDatas[deckSet[P1cont]];
                    P1cont++;
                    break;
                }
            }

        }
    }


    public bool OnHandLess()
    {
        int cont = 0;
        for (int i = 0; i < P1onHands.Length; i++)
        {
            if (P1onHands[i].pieceData == null)
            {
                cont++;
            }
        }
        return cont == P1onHands.Length;
    }






}
