using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogManager : MonoBehaviour
{
    
    public TMP_Text dialogText;
    public GameObject touchArea;
    public GameObject dialogPanel;
    public bool isDialogActive;
    
    private void Awake()
    {
        // Set Dialog invisible
        dialogPanel.SetActive(false);
        isDialogActive = false;
    }

    private void Update()
    {
        // When dialog active
        if (isDialogActive && Managers.touchData.isTouchEnabled)
        {
            if (Managers.touchData.GetTouchObject() == touchArea)
            {
                isDialogActive = false;
                dialogPanel.SetActive(false);
                Debug.Log("[DialogManager] dialog is close");
            }
        }
    }

    public void ShowDialog(String content)
    {
        Debug.Log("door touched");
        isDialogActive = true;
        dialogPanel.SetActive(true);
        dialogText.text = content;
        Managers.touchData.DisableTouchForDuration(0.1f);
    }
}