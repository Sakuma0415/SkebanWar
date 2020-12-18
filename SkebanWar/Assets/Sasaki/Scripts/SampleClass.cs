using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SampleClass : MonoBehaviour
{
    [SerializeField]
	Fade fade = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            fade.FadeIn(1, () =>
            {
                SceneManager.LoadScene("SampleScene2");
            });
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            fade.FadeIn(1, () =>
            {
                SceneManager.LoadScene("SampleScene");
            });
        }
    }
}
