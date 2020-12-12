using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        for(int i=0;i< massOverlaps.Length; i++)
        {
            massOverlaps[i].Init();
        }
    }


    void Update()
    {
        if(massOverlaps[0].BenchNum == -1)
        {
            for (int i = 0; i < games.Length; i++)
            {
                games[i].SetActive(false);
            }
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
    }
}
