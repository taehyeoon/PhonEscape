using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public EasyRoomManager easyRoomManager;
    public bool isDoorLocked;


    private void Awake()
    {
        isDoorLocked = true;
    }

    private void Update()
    {
        if (Managers.touchData.touchedObj == gameObject)
        {
            if (isDoorLocked)
            {
                easyRoomManager.dialogManager.ShowDialog("The handle is rusty and the door won't open. \nLet's find another way to break the handle");
            }
        }
    }
}