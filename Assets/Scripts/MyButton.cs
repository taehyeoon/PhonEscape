using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip clickSound;
    private Button btn;
    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        clickSound = Resources.Load<AudioClip>("Audios/btnClickClip");
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(PlayClickSound);
    }

    public void PlayClickSound()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
    }
}
