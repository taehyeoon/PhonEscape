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
        if (Managers.touchData.touchPhase == TouchPhase.Began &&  Managers.touchData.touchedObj.Equals(clueObject))
        {
            view.SetActive(true);
            backBtn.SetActive(false);
        }else if (Managers.touchData.touchPhase == TouchPhase.Began && Managers.touchData.touchedObj.Equals(outSide))
        {
            view.SetActive(false);
            backBtn.SetActive(true);
        }
    }
}
