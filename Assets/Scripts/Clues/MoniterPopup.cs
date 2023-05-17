using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoniterPopup : MonoBehaviour
{
    public GameObject redStart;
    public GameObject redEnd;
    public LineRenderer redLine;
    private Vector3[] redLinePosList;
    private Vector3 redMiddle;

    private void Awake()
    {
        redLinePosList = new Vector3[2];
        redLine.positionCount = 0;
    }

    private void Update()
    {
        Debug.Log("Taehyeon");
#if UNITY_ANDROID
        if (Managers.touchData.touchPhase == TouchPhase.Began &&  Managers.touchData.touchedObj == redStart && redLine.positionCount == 0)
        {
            Debug.Log("///////////redline start");
            redLine.positionCount = 1;
            redLinePosList[0] = Managers.touchData.touchPosition;
            redLine.SetPosition(0, redLinePosList[0]);
        }else if (Managers.touchData.touchPhase == TouchPhase.Moved && redLine.positionCount >= 1)
        {
            Debug.Log("///////////redline end");

            redLine.positionCount = 2;
            redLinePosList[1] = Managers.touchData.touchPosition;
            redLine.SetPosition(0, redLinePosList[0]);
            redLine.SetPosition(1, redLinePosList[1]);
            
        }
#elif UNITY_EDITOR
        if (Managers.touchData.touchedObj == redStart)
        {
            redLine.positionCount = 1;
            redLine.SetPosition(0, Managers.touchData.touchPosition);
        }else if (Managers.touchData.touchedObj == redEnd && redLine.positionCount == 1)
        {
            redLine.positionCount = 2;
            Debug.Log("endposition : " + Managers.touchData.touchPosition);
            redLine.SetPosition(1, Managers.touchData.touchPosition);
        }
#endif
    }
}
