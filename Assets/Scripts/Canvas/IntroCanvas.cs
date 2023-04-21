using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class IntroCanvas : MonoBehaviour
{
    
    [SerializeField] GameObject[] textLayouts;
    private string[] texts;
    private int curTextIndex;
    private bool isTouchEnable;
    private float fadeHintTime;
    private float fadeTextTime;
    public GameObject hintText;

    public Button tempPlayButton;

    private void Awake()
    {
        init();
        TextValidCheck();
    }

    private void Start()
    {
        StartCoroutine(FadeText(hintText));
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.Space) && isTouchEnable)
        //{
        //    if(curTextIndex < texts.Length)
        //    {
        //        isTouchEnable = false;
        //        curTextIndex++; // outofIndex Exception
        //        Debug.Log("clicked");
        //    }
        //}
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void init()
    {
        curTextIndex = 0;
        isTouchEnable = true;
        fadeHintTime = 0.5f;
        fadeTextTime = 0.5f;

        texts = GameData.data.introTextList;
        foreach(GameObject obj in textLayouts)
        {
            obj.SetActive(false);
        }

        // Temp
        tempPlayButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(EScenes.EasyRoom.ToString());
        });

    }

    private void TextValidCheck()
    {
        if(texts.Length != textLayouts.Length)
        {
            Debug.LogError("The number of elements in textLayouts and texts should be the same");
        }

        foreach(string text in texts)
        {
            if(text == "")
            {
                Debug.LogError("intro text cannot be null");
            }
        }
    }

    // Repeat fadeIn, fadeOut indefinitely
    IEnumerator FadeText(GameObject hintText)
    {
        hintText.SetActive(true);
        CanvasGroup canvasGroup = hintText.GetComponent<CanvasGroup>();
        while (true)
        {
            canvasGroup.alpha = 0;
            yield return new WaitForSeconds(fadeHintTime);

            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / fadeHintTime;
                yield return null;
            }
            yield return new WaitForSeconds(fadeHintTime);

            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeHintTime;
                yield return null;
            }
            yield return new WaitForSeconds(fadeHintTime);
        }
    }

}
