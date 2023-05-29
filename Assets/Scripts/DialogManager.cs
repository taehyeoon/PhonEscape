using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TMP_Text dialogText;
    public GameObject dialogPanel;
    public bool isDialogActive;

    private void Awake()
    {
        // Set Dialog invisible
        dialogPanel.SetActive(false);
        isDialogActive = false;
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
