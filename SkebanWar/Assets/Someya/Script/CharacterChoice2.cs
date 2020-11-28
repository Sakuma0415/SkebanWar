using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterChoice2 : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Character2");
    }
}
