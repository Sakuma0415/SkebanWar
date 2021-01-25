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

    public GameObject SousaText;
    public GameObject NextText;

    bool clickcheck = true;

    float rotSpeed = 0;
    float idou_Speed = 0;

    public int count = 3;
    Rigidbody2D rb;

    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clickcheck == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.rotSpeed = 10;
                this.idou_Speed = 8;

                Invoke("Koin_", 1.0f);

                clickcheck = false;
                SousaText.SetActive(false);

                GameObject obj = GameObject.Find("TouchText");
                Destroy(obj);

                rb.simulated = true;
            }
        }
        if (transform.position.y > 1000)
        {
            this.idou_Speed = 0;

        }
        //else if (transform.position.y < -1)
        //{
        //    this.idou_Speed = 0;
        //    this.rotSpeed = 0;
        //    transform.eulerAngles = new Vector2(0, 0);
        //    transform.position = new Vector2(0, 0);
        //    SousaText.SetActive(true);
        //    NextText.SetActive(true);
        //}

        this.transform.position += new Vector3(0, this.idou_Speed, 0);
        transform.Rotate(this.rotSpeed, 0, 0);

    }
    void Koin_()
    {
        int a = Random.Range(0, 2);
        if (a == 0)
        {
            sampleImage.sprite = Koin1;
            _text.text = "1Pが<size=100>先攻</size>です";
        }
        else
        {
            sampleImage.sprite = Koin2;
            _text.text = "1Pが<size=100>後攻</size>です";
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        this.rotSpeed = 0;
        transform.eulerAngles = new Vector2(0, 0);

        if (other.gameObject.tag == "stage")
        {
            if (count > 0)
            {
                count--;
            }
            if (count == 0)
            {
                rb.simulated = false;
                transform.position = new Vector2(0, 0);
                SousaText.SetActive(true);
                NextText.SetActive(true);
            }
        }
    }

}
