using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnHand : MonoBehaviour
{

    public PieceData pieceData;
    [SerializeField]
    Text text;
    [SerializeField]
    Sprite[] akane;
    [SerializeField]
    Sprite[] iori;
    [SerializeField]
    Sprite[] nana;
    Sprite[] get = new Sprite[1];
    [SerializeField]
    DeckManager deckManager;
    [SerializeField]
    Image image;
    private void Start()
    {
        switch (deckManager.P1deckData.character)
        {
            case DeckData.Character.akane:
                get = akane;
                break;
            case DeckData.Character.iori:
                get = iori;
                break;
            case DeckData.Character.nana :
                get = nana;
                break;
        }
    }




    void Update()
    {
        if (pieceData != null)
        {
            image.color = new Color(1, 1, 1, 1);
            text.text = pieceData.name ;
            switch (pieceData.element)
            {
                case PieceData.Element.R:
                    image.sprite = get[0];
                    break;
                case PieceData.Element.S:
                    image.sprite = get[1];
                    break;
                case PieceData.Element.P:
                    image.sprite = get[2];
                    break;
            }
        }
        else
        {
            image.sprite = null;
            image.color =  new Color(0,0,0,0) ;
            text.text = "";
        }
    }
}
