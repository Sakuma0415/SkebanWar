using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcSelect : MonoBehaviour
{
    [SerializeField]
    GameObject[] games;

    public int mode = 0;
    void Update()
    {
        for(int i = 0; i < games.Length; i++)
        {
            games[i].SetActive(mode == i);
        }
    }
}
