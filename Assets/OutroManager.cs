using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OutroManager : MonoBehaviour
{
    public Button restartBtn;
    public Button quitBtn;
    public Fade fadeWhite;
    public TMP_Text nameTag;
    
    private void Awake()
    {
        nameTag.text = GameData.data.playerName;
        fadeWhite.stopIn = false;
        Invoke(nameof(OffFadeWhite), 2f);
        
        restartBtn.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(EScenes.Menu.ToString());
        });
        
        quitBtn.onClick.AddListener(() =>
        {
            Application.Quit(0);
        });
    }
    
    private void OffFadeWhite()
    {
        fadeWhite.gameObject.SetActive(false);
    }
}
