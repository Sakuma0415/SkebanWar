using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSelect : MonoBehaviour
{

    [SerializeField]
    int select = 0;

    [SerializeField]
    GameObject[] games;


    void Start()
    {
        
    }


    void Update()
    {
        for(int i = 0; i < games.Length; i++)
        {
             games[i].SetActive(i == select);
            
        }
    }
}
