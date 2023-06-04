using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource sfxSource;
    private AudioSource musicSource;
    private AudioSource playerSource;

    public AudioClip doorNotOpen;
    
    
    
    // [Header("SFX")]
    // private AudioClip emptyTouchClip;
    // private AudioClip btnTouchClip;
    // private AudioClip walkClip;
    private Dictionary<string, AudioClip> audioClipDictionary = new Dictionary<string, AudioClip>();
    
    // [Header("Music")]
    // private AudioClip bgmClip;
    
    private void Awake()
    {
        // emptyTouchClip = Resources.Load<AudioClip>("Audios/" + nameof(emptyTouchClip));
        // btnTouchClip = Resources.Load<AudioClip>("Audios/" + nameof(btnTouchClip));
        // walkClip = Resources.Load<AudioClip>("Audios/" + nameof(walkClip));
        //
        // bgmClip = Resources.Load<AudioClip>("Audios/" + nameof(bgmClip));

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
        playerSource = gameObject.AddComponent<AudioSource>();
        playerSource.playOnAwake = false;
        
        AudioClip[] clips = Resources.LoadAll<AudioClip>($"Audios");

        foreach (AudioClip clip in clips)
        {
            audioClipDictionary.Add(clip.name, clip);            
        }
    }

    public void PlayMusic(string clipName)
    {
        if (audioClipDictionary.TryGetValue(clipName, out var value))
        {
            musicSource.clip = value;
            musicSource.Play();
        }
        else
        {
            Debug.Log($"there is no audio clip name : {clipName}");
        }
    }
    
    public void PlaySfx(string clipName)
    {
        if (audioClipDictionary.TryGetValue(clipName, out var value))
        {
            sfxSource.PlayOneShot(value);
        }
        else
        {
            Debug.Log($"there is no audio clip name : {clipName}");
        }
    }
    
    public void PlayDoorNotOpen() => sfxSource.PlayOneShot(doorNotOpen);
}
