using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stagetext : MonoBehaviour
{
    public Text text;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public GameObject text5;
    [HideInInspector]
    public static bool goStageBool;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Stage.TextBool)
        {
            Debug.Log("aassssa");
            Stage.TextBool = true;
            text.text = "戦うナワバリを決めます";
            text1.text = "タッチしてください";
            text2.text = " ";
            text3.text = " ";
            text4.text = " ";
            text5.SetActive(true);

        }
        if (goStageBool)
        {
            if (Input.GetMouseButtonUp(0))
            {
                text.text = "戦うナワバリが決まりました";
                text1.text = "バトルを開始します";
            }
        }        
    }
}
