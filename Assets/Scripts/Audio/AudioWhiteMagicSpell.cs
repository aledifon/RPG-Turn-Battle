using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWhiteMagicSpell : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private AudioClip HealSpellFxClip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void PlayWhiteMagicSpellFx(AudioClip audioClip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void PlayHealSpellFx()
    {
        PlayWhiteMagicSpellFx(HealSpellFxClip);
    }
}
