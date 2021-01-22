using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PcSelect : MonoBehaviour
{
    [SerializeField]
    GameObject[] games;
    [SerializeField]
    SpriteRenderer [] spriteRenderers;
    public int mode = 0;

    public FieldManager.MassOverlap[] massOverlaps=new FieldManager.MassOverlap[1];

    [SerializeField]
    float overlapSize = 0;

    public int attackSelectMode = 0;

    [SerializeField]
    GameObject[] MassMask;

    public int HP=-1;
    [SerializeField]
    Text text;

    public bool IsBrock = false;

    private void Start()
    {
        for(int i=0;i< massOverlaps.Length; i++)
        {
            massOverlaps[i].Init();
        }
    }


    void Update()
    {
        if (IsBrock)
        {
            gameObject.SetActive(false);
        }



        if(HP != -1)
        {
            text.text = "HP:" + HP.ToString();
        }
        else
        {
            text.text = "";
        }



        for (int i = 0; i < games.Length; i++)
        {
            games[i].SetActive(false);
        }
        if (massOverlaps[0].BenchNum == -1)
        {
            //for (int i = 0; i < games.Length; i++)
            //{
            //    games[i].SetActive(false);
            //}
        }
        else
        {
            int count = 0;
            for (int i = 0; i < massOverlaps.Length; i++)
            {
                games[massOverlaps[i].BenchNum +((massOverlaps[i].PlayerNum -1)*6)].SetActive(true);
                games[massOverlaps[i].BenchNum + ((massOverlaps[i].PlayerNum - 1) * 6)].transform.localPosition = new Vector3(-count * overlapSize/2, count * overlapSize, count * overlapSize);
                spriteRenderers[massOverlaps[i].BenchNum + ((massOverlaps[i].PlayerNum - 1) * 6)].sortingOrder = count+1;
                count++;
            }
        }

        switch (attackSelectMode)
        {
            case 0:
                MassMask[0].SetActive(false );
                MassMask[1].SetActive(false);
                break;
            case 1:
                MassMask[0].SetActive(true);
                MassMask[1].SetActive(false );
                break;
            case 2:
                MassMask[0].SetActive(false );
                MassMask[1].SetActive(true);
                break;

        }




    }
}
