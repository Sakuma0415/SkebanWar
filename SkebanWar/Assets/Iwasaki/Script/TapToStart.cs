using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToStart : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip sound;
    [SerializeField]
    private Animator anim;
    public void Event()
    {
        audioSource.PlayOneShot(sound);
        anim.SetTrigger("FadeOut");
    }    
}
