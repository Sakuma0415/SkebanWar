using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimFunctions : MonoBehaviour
{
    [SerializeField]
    private GameObject cutInImage;
    public void AnimEvent()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void CutInImageTrue()
    {
        cutInImage.SetActive(true);
    }
    public void CutInImageFalse()
    {
        cutInImage.SetActive(false);
    }
}
