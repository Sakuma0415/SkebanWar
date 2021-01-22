using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTest : MonoBehaviour
{
    [SerializeField]
    int douka = 0;

    [SerializeField]
    GameObject[] Coin;
    [SerializeField]
    private GameObject senkouImage;
    [SerializeField]
    private GameObject koukouImage;

    public void Click()
    {
        douka = Random.Range(0, Coin.Length);
        Instantiate(Coin[douka], transform.position, transform.rotation);
    }

    void Update()
    {
        for (int i = 0; i < Coin.Length; i++)
        {
            if(douka == 0)
            {
                senkouImage.SetActive(true);
            }
            else if(douka == 1)
            {
                koukouImage.SetActive(true);
            }
        }
    }
}
