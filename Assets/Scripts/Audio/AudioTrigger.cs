using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();        
    }

    public void PlayEventFx()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
