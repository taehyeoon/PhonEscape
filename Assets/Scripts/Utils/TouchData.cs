using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchData : MonoBehaviour
{
    public bool isTouching;
    public GameObject touchedObj;
    public Vector3 touchPosition;
    public TouchPhase touchPhase;
    
    private void Update()
    {
        GetTouchedData();
    }

    private void SetDefault()
    {
        touchedObj = null;
        touchPosition = Vector3.one;
        touchPhase = TouchPhase.Canceled;
    }

    private void GetTouchedData()
    {
        if (Input.touchCount == 0)
        {
            isTouching = false;
            SetDefault();
        }
        else if(Input.touchCount > 0)
        {
            isTouching = true;
            
            Touch touch = Input.GetTouch(0);
            
            // 터치 위치를 스크린 좌표에서 월드 좌표로 변환
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            // 2D 레이캐스트 생성
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

            touchPosition = touchPos;
            touchPhase = touch.phase;

            // 레이캐스트가 물체에 부딪힌 경우
            if (hit.collider != null)
            {
                // 부딪힌 물체 정보 출력
                Debug.Log("Touched object: " + hit.collider.gameObject.name);
                touchedObj = hit.collider.gameObject;
            }
            else
            {
                touchedObj = null;
            }
        }
    }
}
