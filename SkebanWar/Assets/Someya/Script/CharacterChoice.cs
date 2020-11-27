using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChoice : MonoBehaviour
{
    private Button Character1;
    private Button Character2;
    private Button Character3;

    // Start is called before the first frame update
    void Start()
    {
        // 各ボタンコンポーネントの取得
        Character1 = GameObject.Find("/Canvas/Character1").GetComponent<Button>();
        Character2 = GameObject.Find("/Canvas/Character2").GetComponent<Button>();
        Character3 = GameObject.Find("/Canvas/Character3").GetComponent<Button>();
    }
}
