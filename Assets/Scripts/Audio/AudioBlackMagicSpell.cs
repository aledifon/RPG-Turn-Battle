using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBlackMagicSpell : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private AudioClip FireSpellFxClip;    

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();        
    }
    private void PlayBlackMagicSpellFx(AudioClip audioClip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void PlayFireSpellFx()
    {
        PlayBlackMagicSpellFx(FireSpellFxClip);
    }  
}
