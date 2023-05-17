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
        Debug.Log("TouchData");
        Debug.Log("isTouching : " + isTouching);
        Debug.Log("object : " + (touchedObj == null? "null" : touchedObj.name));
        Debug.Log("pos : " + touchPosition);
        Debug.Log("phase : " + touchPhase);
    }

    private void SetDefault()
    {
        touchPosition = Vector3.one;
        touchedObj = null;
    }

    private void GetTouchedData()
    {
#if UNITY_ANDROID
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
/*
            if (Input.touchCount <= 0)
            {
                SetDefault();
                return;
            }
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began)
            {
                SetDefault();
                return;
            }
            
            // 터치 위치를 스크린 좌표에서 월드 좌표로 변환
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            // 2D 레이캐스트 생성
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

            // 레이캐스트가 물체에 부딪힌 경우
            if (hit.collider != null)
            {
                // 부딪힌 물체 정보 출력
                Debug.Log("Touched object: " + hit.collider.gameObject.name);
                touchPosition = touchPos;
                touchedObj = hit.collider.gameObject;
            }
            else
            {
                SetDefault();
            }
            */
#elif UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 버튼 클릭을 감지합니다. 다른 버튼에 맞게 수정할 수 있습니다.
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedObject = hit.collider.gameObject;
                touchPosition = clickedObject.transform.position;
                touchedObj = clickedObject;
                // 클릭한 오브젝트의 정보를 사용하거나 출력합니다.
                Debug.Log("Clicked Object: " + clickedObject.name);
                return;
            }

            SetDefault();
        }
#endif

    }
}
