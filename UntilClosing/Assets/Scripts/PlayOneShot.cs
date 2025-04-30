using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShot : MonoBehaviour
{
    //i love playoneshot and unityevents!!!

    public AudioClip audio;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayOneShotMethod()
    {
        audioSource.PlayOneShot(audio, 1);
    }
}
