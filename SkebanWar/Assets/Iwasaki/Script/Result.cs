using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    private Text redText;
    private Text blueText;
    void Start()
    {
        blueText = GameObject.FindGameObjectWithTag("BlueText").GetComponent<Text>();
        redText = GameObject.FindGameObjectWithTag("RedText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {        
        blueText.text = Random.Range(1, 30).ToString();
        redText.text = Random.Range(1, 30).ToString();
    }
}
