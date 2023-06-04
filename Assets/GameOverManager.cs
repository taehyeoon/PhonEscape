using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Button restartBtn;
    public Button quitBtn;
    public Fade fadeBlack;
    
    private void Awake()
    {
        fadeBlack.stopIn = false;
        
        restartBtn.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(EScenes.Menu.ToString());
        });
        
        quitBtn.onClick.AddListener(() =>
        {
            Application.Quit(0);
        });
    }

    private void Update()
    {
        Invoke(nameof(OffFadeBlack), 2f);
    }

    private void OffFadeBlack()
    {
        fadeBlack.gameObject.SetActive(false);
    }
}
