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
    private Vector3 redMiddle;
    public bool isRedLineDrawing;

    private void Awake()
    {
        redLine.positionCount = 0;
    }

    private void Update()
    {
#if UNITY_ANDROID
        if (Managers.touchData.touchPhase == TouchPhase.Began && Managers.touchData.touchedObj == redStart)
        {
            Debug.Log("touch redStart");
            isRedLineDrawing = true;
            redLine.positionCount = 1;
            redLine.SetPosition(0, redStart.transform.position);
            Debug.Log("Start point : " + redStart.transform.position);
        }else if (Managers.touchData.touchPhase == TouchPhase.Moved && isRedLineDrawing)
        {
            Debug.Log("touch Moved");
            redLine.positionCount = 2;
            var currentPos = new Vector3(Managers.touchData.touchPosition.x, Managers.touchData.touchPosition.y, -4); 
            redLine.SetPosition(1, currentPos);
            Debug.Log("current pos : " + currentPos);
        }else if (Managers.touchData.touchPhase == TouchPhase.Ended)
        {
            isRedLineDrawing = false;
            redLine.positionCount = 0;
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
