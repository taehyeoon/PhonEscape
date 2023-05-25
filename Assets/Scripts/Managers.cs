using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers instance;
    public static AudioManager audioManager;
    public static TouchData touchData;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

        GameObject emptyObj = new GameObject();
        GameObject audioManagerObj = Instantiate(emptyObj, transform);
        audioManagerObj.name = "Audio Manager";
        audioManager = audioManagerObj.AddComponent<AudioManager>();
        
        GameObject touchDataObj = Instantiate(emptyObj, transform);
        touchDataObj.name = "Touch Data";
        touchData = touchDataObj.AddComponent<TouchData>();
    }
}
