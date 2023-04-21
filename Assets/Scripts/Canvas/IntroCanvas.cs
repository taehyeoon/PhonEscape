using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class IntroCanvas : MonoBehaviour
{    
    [SerializeField] TMP_Text hintTextLayout;
    [SerializeField] TMP_Text[] textLayouts;
    private string[] texts;
    private int curTextIndex;
    private float fadeHintTime;
    private float fadeTextTime;

    private bool isCoroutineRunning;
    private bool isTouchEnable;

    private void Awake()
    {
        TextValidCheck();        
        init();
    }

    private void Start()
    {
        StartCoroutine(FadeInOutText(hintTextLayout));
    }

    private void Update()
    {
        isTouchEnable = !isCoroutineRunning;

        if (!isTouchEnable)
            return;

        if (!CheckTouched())
            return;

        if (CheckedIntroEnd())
        {
            SceneLoader.LoadScene(GameData.data.selectedLevelScene.ToString());
        }

        if (curTextIndex < texts.Length)
        {
            if (curTextIndex != 0)
            {
                StartCoroutine(FadeOutText(textLayouts[curTextIndex - 1]));
            }
            StartCoroutine(FadeInText(textLayouts[curTextIndex]));
            curTextIndex++;
        }   
    }

    private bool CheckTouched()
    {
        return Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space);
    }

    private bool CheckedIntroEnd()
    {
        TMP_Text lastText = textLayouts[textLayouts.Length - 1];
        if(lastText.gameObject.activeSelf && lastText.GetComponent<CanvasGroup>().alpha == 1)
            return true;
        else
            return false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void init()
    {
        curTextIndex = 0;
        isCoroutineRunning = false;
        isTouchEnable = true;

        texts = GameData.data.introTextList;
        hintTextLayout.text = GameData.data.hintText;
        fadeHintTime = GameData.data.hintFadeInterval;
        fadeTextTime = GameData.data.textFadeInterval;

        // Set text active false
        hintTextLayout.gameObject.SetActive(false);
        for(int i =0; i< texts.Length; i++)
        {
            textLayouts[i].text = texts[i];
            textLayouts[i].gameObject.SetActive(false);
        }
    }

    private void TextValidCheck()
    {
        string[] listForChecking = GameData.data.introTextList;

        foreach(string text in listForChecking)
        {
            if(text == "")
            {
                Debug.LogError("intro text cannot be null");
            }
        }

        if(listForChecking.Length != textLayouts.Length)
        {
            Debug.LogError("The number of elements in textLayouts and texts should be the same");
        }
    }

    // Repeat fadeIn, fadeOut indefinitely
    IEnumerator FadeInOutText(TMP_Text hintText)
    {
        hintText.gameObject.SetActive(true);
        CanvasGroup canvasGroup = hintText.GetComponent<CanvasGroup>();
        while (true)
        {
            canvasGroup.alpha = 0;

            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / fadeHintTime;
                yield return null;
            }

            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeHintTime;
                yield return null;
            }
        }
    }

    IEnumerator FadeInText(TMP_Text target)
    {
        isCoroutineRunning = true;

        target.gameObject.SetActive(true);
        CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        while(canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / fadeTextTime;
            yield return null;
        }

        isCoroutineRunning = false;
    }

    IEnumerator FadeOutText(TMP_Text target)
    {
        isCoroutineRunning = true;

        target.gameObject.SetActive(true);
        CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeTextTime;
            yield return null;
        }

        isCoroutineRunning = false;
    }
}
