using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimFunctions : MonoBehaviour
{
    public void AnimEvent()
    {
        SceneManager.LoadScene("CharacterSelect");
    }
}
