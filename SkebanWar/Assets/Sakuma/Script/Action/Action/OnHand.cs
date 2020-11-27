using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnHand : MonoBehaviour
{

    public PieceData pieceData;
    [SerializeField]
    Text text;
    void Update()
    {
        if (pieceData != null)
        {
            text.text = pieceData.name ;
        }
        else
        {
            text.text = "";
        }
    }
}
