using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Lock : MonoBehaviour
{
    public GameObject[] btns;
    public bool[] isBtnPushs;
    public bool isLocked;

    public EasyRoomManager easyRoomManager;
    public SpriteRenderer sr;
    [SerializeField] private Sprite unlockSprite;

    public bool isDialogShow;
    private void Awake()
    {
        isLocked = true;
        isDialogShow = false;
        for (int i = 0; i < isBtnPushs.Length; i++)
        {
            isBtnPushs[i] = false;
        }
    }

    private void Update()
    {
        if (!isLocked)
        {

            return;
        }
        
        // Update btn state
        for(int i =0; i<btns.Length; i++)
        {
            if (Managers.touchData.touchPhase== TouchPhase.Began &&  Managers.touchData.touchedObj == btns[i])
            {
                Debug.Log("[lock] " + (i+1) + " btn touch");
                bool curState = isBtnPushs[i];
                if (!curState)
                {
                    Debug.Log("[lock] button " + (i+1) + " push");

                    btns[i].GetComponent<SpriteRenderer>().color = Color.black;
                    isBtnPushs[i] = true;
                }
                else
                {
                    Debug.Log("[lock] button " + (i+1) + " unpush");

                    btns[i].GetComponent<SpriteRenderer>().color = Color.white;
                    isBtnPushs[i] = false;
                }
            }
        }
        
        // check answer
        if (CheckAnswer())
        {
            if (!isDialogShow)
            {
                isDialogShow = true;
                easyRoomManager.dialogManager.ShowDialog("Get a hammer in your hand");
            }
            isLocked = false;
            sr.sprite = unlockSprite;
            easyRoomManager.hasHammer = true;
            Debug.Log("[Lock] is unlocked");
        }
    }

    private bool CheckAnswer()
    {
        // 2,5 8
        return
            !isBtnPushs[0] &&
            isBtnPushs[1] &&
            !isBtnPushs[2] &&
            !isBtnPushs[3] &&
            isBtnPushs[4] &&
            !isBtnPushs[5] &&
            !isBtnPushs[6] &&
            isBtnPushs[7];
    }
}
