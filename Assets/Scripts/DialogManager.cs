using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogManager : MonoBehaviour
{
    public TMP_Text dialogText;
    public GameObject dialogPanel;
    public bool isDialogActive;

    public bool isDoorDialogExecuted;
    private void Awake()
    {
        // Set Dialog invisible
        dialogPanel.SetActive(false);
        isDialogActive = false;
    }

    private void Update()
    {
        if (isDialogActive && Managers.touchData.isTouchEnabled)
        {
            if (Managers.touchData.GetTouchObject() == dialogPanel)
            {
                isDialogActive = false;
                dialogPanel.SetActive(false);
                Debug.Log("[DialogManager] dialog is close");
            }
        }
        else
        {
            GameObject touchedObj = Managers.touchData.GetTouchObject();
            if (touchedObj != null)
            {
                if (touchedObj.name == "door" && !isDoorDialogExecuted)
                {
                    Debug.Log("door touched");
                    isDialogActive = true;
                    dialogPanel.SetActive(true);
                    dialogText.text = "The handle is rusty and the door won't open.";
                    isDoorDialogExecuted = true;
                    Managers.touchData.DisableTouchForDuration(2f);
                }
            }
        }
        

    }

    public void UpdateDialog()
    {
        if (Managers.touchData.touchPhase == TouchPhase.Began)
        {
            if (isDialogActive)
            {
                if (Managers.touchData.touchedObj.Equals(dialogPanel))
                {
                    isDialogActive = false;
                    dialogPanel.SetActive(isDialogActive);
                }
            }
            else
            {
                if (Managers.touchData.touchedObj != null)
                {
                    isDialogActive = true;
                    dialogPanel.SetActive(isDialogActive);
                    // dialogText.text = Managers.touchData.touchedObj.name;
                    dialogText.text = "It stinks. \nLet's find a place to throw away trash.";
                    
                }
            }
        }


        if (Managers.touchData.touchPhase == TouchPhase.Began)
        {
            if (isDialogActive)
            {
                isDialogActive = false;
                dialogPanel.SetActive(false);
            }
            else
            {
                isDialogActive = true;
                dialogPanel.SetActive(true);
                GameObject touchedObj = Managers.touchData.touchedObj;
                if (touchedObj != null)
                {
                    dialogText.text = touchedObj.name;
                }
                else
                {
                    dialogText.text = "null";
                }
            }
        }
    }
}
