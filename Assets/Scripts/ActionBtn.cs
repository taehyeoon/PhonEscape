using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public enum EBtnState
{
    A,
    Wall,
    Trash,
    TrashCan,
}
public class ActionBtn : MonoBehaviour
{
    public EBtnState curState;
    public TMP_Text btnText;
    public EWall scannedWall;
    private void Awake()
    {
        curState = EBtnState.A;
        btnText.SetText("A");
        
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (curState == EBtnState.Wall)
            {
                Debug.Log("in actionBtn");
                EasyRoomManager.loadWall(scannedWall);
            }
        });
    }

    public void SetBtn(EBtnState state, EWall wall = EWall.East)
    {
        curState = state;
        scannedWall = wall;
        btnText.SetText(state.ToString());
    }
}
