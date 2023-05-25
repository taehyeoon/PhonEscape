using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BaseRoomManager : MonoBehaviour
{
    [Header("Room, Walls")]
    public GameObject room;
    public GameObject walls;
    public GameObject north;
    public GameObject south;
    public GameObject west;
    public GameObject east;
    
    [Header("Light")]
    public Light2D wallGlobalLight;
    public Light2D roomGlobalLight;
    
    public static Action<EWall> loadWall;
    public static System.Action loadRoom;

    private GameObject curState;

    public Player player;
    
    protected virtual void Awake()
    {
        wallGlobalLight.gameObject.SetActive(false);
        // Managers.audioManager.PlayMusic("bgmClip");
        curState = room;
        room.SetActive(true);
        walls.SetActive(false);
        north.SetActive(false);
        south.SetActive(false);
        west.SetActive(false);
        east.SetActive(false);

        loadWall = (targetWall) =>
        {
            roomGlobalLight.gameObject.SetActive(false);
            curState.SetActive(false);
            
            switch (targetWall)
            {
                case EWall.North:
                    curState = north;
                    break;
                case EWall.South:
                    curState = south;
                    break;
                case EWall.West:
                    curState = west;
                    break;
                case EWall.East:
                    curState = east;
                    break;
                default:
                    Debug.LogError("Inappropriate input");
                    break;
            }
            walls.SetActive(true);
            curState.SetActive(true);
            wallGlobalLight.gameObject.SetActive(true);
        };

        loadRoom = () =>
        {
            wallGlobalLight.gameObject.SetActive(false);
            curState.SetActive(false);
            walls.SetActive(false);
            curState = room;
            curState.SetActive(true);
            roomGlobalLight.gameObject.SetActive(true);
        };
    }
}
