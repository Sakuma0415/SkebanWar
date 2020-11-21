using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public DeckData deckData;

    public int[] deckSet;

    public int[] hand = new int[2];



    void Start()
    {
        int[] data = new int[] {0,1,2,3,4,5 };
        deckSet = data.OrderBy(i => Guid.NewGuid()).ToArray();
    }

    void Update()
    {
        
    }
}
