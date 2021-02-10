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
    void Start()
    {
        SoundManager.Instans.FadeInBGM("Explosion");
    }
    //public void Event()
    //{
    //    audioSource.PlayOneShot(sound);
    //    anim.SetTrigger("FadeOut");
    //}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(sound);
            anim.SetTrigger("FadeOut");
        }
    }
}
