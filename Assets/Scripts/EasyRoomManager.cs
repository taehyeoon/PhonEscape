using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyRoomManager : MonoBehaviour
{
    public GameObject room;
    public GameObject walls;
    public GameObject north;
    public GameObject south;
    public GameObject west;
    public GameObject east;

    public static System.Action<EWall> loadWall;
    public static System.Action loadRoom;

    private GameObject curState;

    private void Awake()
    {
        curState = room;
        room.SetActive(true);
        walls.SetActive(false);
        north.SetActive(false);
        south.SetActive(false);
        west.SetActive(false);
        east.SetActive(false);

        loadWall = (targetWall) =>
        {
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
        };

        loadRoom = () =>
        {
            curState.SetActive(false);
            walls.SetActive(false);
            curState = room;
            curState.SetActive(true);
        };
    }


}
