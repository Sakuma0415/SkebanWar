using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Koinmanager : MonoBehaviour
{
    public Image sampleImage;
    public Sprite Koin1;
    public Sprite Koin2;
    public Text text;
    public Text text2;
    public Text text3;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            transform.position = new Vector3(0, 1600, 0);

            Invoke("Koin_", 2.0f);
        }
    }
    void Koin_()
    {
        transform.position = new Vector3(0, 0, 0);
        int a = Random.Range(0, 2);
        if (a == 0)
        {
            text.text = "1Pが後攻です";
            sampleImage.sprite = Koin1;
        }
        else
        {
            text.text = "1Pが先攻です";
            sampleImage.sprite = Koin2;
        }

    }
}
