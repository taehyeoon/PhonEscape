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
    public Player player;
    
    public EBtnState curState;
    public TMP_Text btnText;
    public GameObject frontObject;
    
    private void Awake()
    {
        curState = EBtnState.A;
        btnText.SetText("A");
        
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (curState == EBtnState.Wall)
            {
                Debug.Log("in actionBtn");
                BaseRoomManager.loadWall(frontObject.GetComponent<Wall>().orientation);
                return;
            }

            if (curState == EBtnState.Trash)
            {
                frontObject.transform.SetParent(player.trashHolder.transform);
                // frontObject.transform.position = Vector3.zero;
                return;
            }
            
        });
    }

    public void SetBtn(EBtnState state, GameObject frontObj = null)
    {
        curState = state;
        frontObject = frontObj;
        btnText.SetText(state.ToString());
    }
}
