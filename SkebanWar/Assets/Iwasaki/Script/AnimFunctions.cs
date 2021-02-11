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

    public void TextSound()
    {
        SoundManager.Instans.PlaySE(3);
    }
    public void PlateSound()
    {
        SoundManager.Instans.PlaySE(6);
    }
    public void CharSound()
    {
        SoundManager.Instans.PlaySE(5);
    }
}
