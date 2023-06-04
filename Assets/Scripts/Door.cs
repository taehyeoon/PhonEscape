using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class Door : MonoBehaviour
{
    public EasyRoomManager easyRoomManager;
    public bool isDoorLocked;
    public Lock lockScript;

    public GameObject doorOrigin;
    public GameObject doorBody;
    public GameObject doorFragment;

    public Light2D doorLight;
    public float curLightIntensity;
    public float maxLightIntensity;

    private bool isStartCoroutine;

    public bool isDoorOpen;

    public AudioManager am;
    private void Awake()
    {
        isDoorLocked = true;
        
        doorOrigin.SetActive(true);
        doorBody.SetActive(false);
        doorFragment.SetActive(false);
        doorLight.intensity = 0;
        curLightIntensity = 0;
        maxLightIntensity = 10f;
        doorLight.gameObject.SetActive(false);
        isStartCoroutine = false;
        isDoorOpen = false;
    }

    private void Update()
    {
        if(isDoorOpen) return;

        if (Managers.touchData.touchedObj == doorOrigin)
        {
            if(easyRoomManager.hasHammer)
            {
                Debug.Log("[Door] Hammer");
                isDoorOpen = true;
                if (!isStartCoroutine)
                {
                    Debug.Log("[Door] start light coroutine");
                    doorLight.gameObject.SetActive(true);
                    doorOrigin.SetActive(false);
                    doorBody.SetActive(true);
                    doorFragment.SetActive(true);
                    isStartCoroutine = true;
                    StartCoroutine(LightOn());
                }

            }
            else
            {
                Debug.Log("[Door] message");
                am.PlayDoorNotOpen();
                easyRoomManager.dialogManager.ShowDialog("The handle is rusty and the door won't open.\n" +
                                                         "Let's find another way to break the handle");
            }
        }
    }

    private IEnumerator LightOn()
    {
        while (curLightIntensity < maxLightIntensity)
        {
            curLightIntensity += Time.deltaTime * 2f;
            doorLight.intensity = curLightIntensity;
            yield return null;
        }

        doorLight.intensity = maxLightIntensity;
        Debug.Log("[Door] coroutine end");
        easyRoomManager.fadeWhite.gameObject.SetActive(true);
        easyRoomManager.fadeWhite.stopOut = false;
        Invoke(nameof(GotoOutro), 2f);
    }
    
    void GotoOutro()
    {
        SceneLoader.LoadScene(EScenes.Outro.ToString());
    }

    
}