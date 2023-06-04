using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    // Clicking this object opens a pop-up window
    public GameObject clueObject;
    
    // Popup view
    public GameObject view;
    public GameObject outSide;
    public GameObject backBtn;

    protected void Awake()
    {
        view.SetActive(false);
    }

    protected void Update()
    {
        if(clueObject.Equals(Managers.touchData.GetTouchObject()))
        {
            view.SetActive(true);
            backBtn.SetActive(false);
        }else if (outSide.Equals(Managers.touchData.GetTouchObject()))
        {
            Debug.Log("[popup] outSide touch");
            view.SetActive(false);
            backBtn.SetActive(true);
        }
    }
}
