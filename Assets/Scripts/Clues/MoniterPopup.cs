using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class MoniterPopup : MonoBehaviour
{
    public GameObject[] starts;
    public GameObject[] ends;
    public LineRenderer[] lines;
    public bool[] isLineComplete;
    public bool isDrawingLine;
    public int currentLineIndex;

    public GameObject clue;
    public GameObject moniterLight;
    
    private void Update()
    {
        // No lines currently being drawn
        if (currentLineIndex == -1)
        {
            LineBegin();
        }
        // There is a line that is being drawn now
        else
        {
            LineMove();
            LineEnd();
        }

        if (CheckIsClear())
        {
            clue.SetActive(true);
            moniterLight.SetActive(true);
        }
    }

    private bool CheckIsClear()
    {
        foreach (var lineComplete in isLineComplete)
        {
            if (!lineComplete)
            {
                return false;
            }
        }

        return true;
    }
    private void LineEnd()
    {
        if (isLineComplete[currentLineIndex]) return;
        
        if (Managers.touchData.touchPhase == TouchPhase.Ended)
        {
            // the right destination
            if (Managers.touchData.touchedObj == ends[currentLineIndex])
            {
                lines[currentLineIndex].SetPosition(1, ends[currentLineIndex].transform.position);
                isLineComplete[currentLineIndex] = true;
                Debug.Log("line draw end  " + currentLineIndex);
            }
            else
            {
                lines[currentLineIndex].positionCount = 0;
            }

            currentLineIndex = -1;
            isDrawingLine = false;
        }
    }

    private void LineMove()
    {
        if (Managers.touchData.touchPhase == TouchPhase.Moved && isDrawingLine)
        {
            var currentPos = new Vector3(Managers.touchData.touchPosition.x, Managers.touchData.touchPosition.y, -4);

            lines[currentLineIndex].positionCount = 2;
            lines[currentLineIndex].SetPosition(1, currentPos);
            Debug.Log("move Touch");
        }
    }

    private void LineBegin()
    {
        for (int i = 0; i < starts.Length; i++)
        {
            // Pass the line that's already drawn
            if (isLineComplete[i])
            {
                Debug.Log("Begin line Complete : " + isLineComplete[i]);
                continue;
            }
            
            if (Managers.touchData.touchPhase == TouchPhase.Began && Managers.touchData.touchedObj == starts[i])
            {
                currentLineIndex = i;
                isDrawingLine = true;
                lines[i].positionCount = 1;
                lines[i].SetPosition(0, starts[i].transform.position);
                Debug.Log("Begin line draw : " + currentLineIndex);
                return;
            }
        }
        currentLineIndex = -1;
    }
}
