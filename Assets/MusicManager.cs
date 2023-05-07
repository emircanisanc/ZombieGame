using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource audioSource;

    private bool isClipPlaying = false;

    void Start()
    {
        PlayRandomMusic();
    }

    void Update()
    {
        if (!isClipPlaying && audioSource.isPlaying)
        {
            isClipPlaying = true;
        }

        if (isClipPlaying && !audioSource.isPlaying)
        {
            OnClipEnd();
        }
    }

    private void OnClipEnd()
    {
        isClipPlaying = false;
        Invoke("PlayRandomMusic", 5f);
    }

    private void PlayRandomMusic()
    {
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
        isClipPlaying = true;
    }
}
