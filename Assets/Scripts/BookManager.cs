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
    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount==1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Book"))
            {
                // ºÎµúÈù ¹°Ã¼ Á¤º¸ Ãâ·Â
                //Debug.Log("Touched object: " + hit.collider.gameObject.name);
                selectBook(hit.collider.gameObject);
                BookOrder();
                if (flag == true)
                {
                    Debug.Log("Order is set");
                }
            }
        }
    }
    void selectBook(GameObject book)
    {
        if(selectedBook == null)
        {
            selectedBook = book;
        }
        else if (selectedBook.name != book.name)
        {
            Vector3 tempPos = selectedBook.transform.position;
            //Debug.Log("Before Swap: selectedBook = " + selectedBook.transform.position + ", book = " + book.transform.position);
            selectedBook.transform.position = new Vector3(book.transform.position.x, selectedBook.transform.position.y, selectedBook.transform.position.z);
            book.transform.position = new Vector3(tempPos.x, book.transform.position.y, book.transform.position.z);
            //Debug.Log("After Swap: selectedBook = " + selectedBook.transform.position + ", book = " + book.transform.position);
            selectedBook = null;
        }
        else
        {
            selectedBook = null;
        }

    }
    void BookOrder()
    {
        if(book1.transform.position.x<book2.transform.position.x && book2.transform.position.x<book3.transform.position.x && book3.transform.position.x<book4.transform.position.x && book5.transform.position.x < book6.transform.position.x)
        {
            flag = true;
        }


    }
}
