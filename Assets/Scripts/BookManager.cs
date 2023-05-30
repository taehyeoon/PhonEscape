using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public GameObject book1;
    public GameObject book2;
    public GameObject book3;
    public GameObject book4;
    public GameObject book5;
    public GameObject book6;
    private GameObject selectedBook = null;
    private bool flag = false;
    private SpriteRenderer spriteRenderer;
    public Color originalColor=Color.white;
    public Color highlightColor=Color.black;

    public GameObject questionMark;
    private void Awake()
    {
        questionMark.SetActive(false);
    }

    void Update()
    {
        if(Input.touchCount==1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Book"))
            {
                // �ε��� ��ü ���� ���
                //Debug.Log("Touched object: " + hit.collider.gameObject.name);
                selectBook(hit.collider.gameObject);
                BookOrder();
                if (flag == true)
                {
                    Debug.Log("Order is set");
                }
            }
        }

        if (flag)
        {
            ShowQuestionMark();
        }
    }
    void selectBook(GameObject book)
    {
        if(selectedBook == null)
        {
            selectedBook = book;
            spriteRenderer = selectedBook.GetComponent<SpriteRenderer>();
            spriteRenderer.color=highlightColor;
            selectedBook.transform.position = new Vector3(selectedBook.transform.position.x, selectedBook.transform.position.y+1.0f,selectedBook.transform.position.z);
        }
        else if (selectedBook.name != book.name)
        {
            Vector3 tempPos = selectedBook.transform.position;
            //Debug.Log("Before Swap: selectedBook = " + selectedBook.transform.position + ", book = " + book.transform.position);
            selectedBook.transform.position = new Vector3(book.transform.position.x, tempPos.y-1.0f, selectedBook.transform.position.z);
            book.transform.position = new Vector3(tempPos.x, book.transform.position.y, book.transform.position.z);
            spriteRenderer = selectedBook.GetComponent<SpriteRenderer>();
            spriteRenderer.color = originalColor;
            //Debug.Log("After Swap: selectedBook = " + selectedBook.transform.position + ", book = " + book.transform.position);
            selectedBook = null;
        }
        else
        {     
            spriteRenderer = selectedBook.GetComponent<SpriteRenderer>();
            spriteRenderer.color = originalColor;
            selectedBook.transform.position = new Vector3(selectedBook.transform.position.x, selectedBook.transform.position.y - 1.0f, selectedBook.transform.position.z);
            selectedBook = null;
        }

    }
    void BookOrder()
    {
        float[] bookPositions = new float[6];
        bookPositions[0] = book1.transform.position.x;
        bookPositions[1] = book2.transform.position.x;
        bookPositions[2] = book3.transform.position.x;
        bookPositions[3] = book4.transform.position.x;
        bookPositions[4] = book5.transform.position.x;
        bookPositions[5] = book6.transform.position.x;

        for (int i = 0; i < bookPositions.Length - 1; i++)
        {
            if (bookPositions[i] >= bookPositions[i + 1])
            {
                flag = false;
                return;
            }
        }

        flag = true;
    }

    private void ShowQuestionMark()
    {
        questionMark.SetActive(true);
    }
}
