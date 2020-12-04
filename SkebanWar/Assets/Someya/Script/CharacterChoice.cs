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
        select = 0;
    }

    void Update()
    {
        for (int i = 0; i < Character.Length; i++)
        {
            Character[i].SetActive(i == select); 
        }
    }
}
