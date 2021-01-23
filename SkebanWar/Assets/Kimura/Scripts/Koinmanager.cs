using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Koinmanager : MonoBehaviour
{
    public Image sampleImage;
    public Sprite Koin1;
    public Sprite Koin2;
    public Text _text;
    public GameObject text;

    bool test = true;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (test == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                transform.position = new Vector3(0, 1600, 0);
                Invoke("Koin_", 2.0f);
                test = false;
            }
        }
    }
    void Koin_()
    {
        transform.position = new Vector3(0, 0, 0);
        int a = Random.Range(0, 2);
        GameObject obj = GameObject.Find("Text1");
        Destroy(obj);
        if (a == 0)
        {
            sampleImage.sprite = Koin1;
            _text.text = "1Pが<size=100>後攻</size>です";
        }
        else
        {
            sampleImage.sprite = Koin2;
            _text.text = "1Pが<size=100>先攻</size>です";
        }
        text.SetActive(true);
    }
}
