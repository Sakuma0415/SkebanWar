using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterChoice : MonoBehaviour
{
    [SerializeField]
    int select = 0;

    [SerializeField]
    GameObject[] Character;

    [SerializeField]
    int choice = 0;

    [SerializeField]
    GameObject[] Decsion;

    // ボタンを押したら選んだキャラクターを描画
    public void C1Change()
    {
        select = 1;
    }

    public void C2Change()
    {
        select = 2;
    }

    public void C3Change()
    {
        select = 3;
    }

    // いいえを押したら選択に戻る
    public void Cancel()
    {
        choice = 1;
        select = 0;
    }

    public void OK()
    {
        choice = 2;
    }

    void Update()
    {
        for (int i = 0; i < Character.Length; i++)
        {
            Character[i].SetActive(i == select); 
        }
        for (int j = 0; j < Decsion.Length; j++)
        {
            Decsion[j].SetActive(j == choice);
        }
    }
}
