using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Button restartBtn;
    public Button quitBtn;

    private void Awake()
    {
        restartBtn.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(EScenes.Menu.ToString());
        });
        
        quitBtn.onClick.AddListener(() =>
        {
            Application.Quit(0);
        });
    }
}
