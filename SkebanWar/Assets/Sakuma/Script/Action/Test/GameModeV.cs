using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameModeV : MonoBehaviour
{

    [SerializeField]
    Text text;

    void Start()
    {
        
    }

    void Update()
    {
        text.text  = Progress.Instance .gameMode.ToString ();
    }
}
