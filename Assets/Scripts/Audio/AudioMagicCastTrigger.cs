using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMagicCastTrigger : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private AudioClip blackMagicCastFxClip;
    [SerializeField] private AudioClip whiteMagicCastFxClip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();        
    }
    private void PlayAudioFx(AudioClip audioClip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void PlayBlackMagicCastFx()
    {
        PlayAudioFx(blackMagicCastFxClip);
    }
    public void PlayWhiteMagicCastFx()
    {
        PlayAudioFx(whiteMagicCastFxClip);
    }
}
