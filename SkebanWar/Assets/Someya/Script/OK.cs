using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OK : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Action");
    }
}
