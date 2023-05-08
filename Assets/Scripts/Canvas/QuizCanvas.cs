using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizCanvas : MonoBehaviour
{
    public Button temp_success;        
    public Button temp_fail;

    private Sprite[] snsImages;
    private Sprite nonSnsImage;

    public float timeLimit;
    public float remainingTime;

    public TMP_Text timeText;
    public Button[] optionBtns;

    private void Awake()
    {
        temp_success.onClick.AddListener(() =>
        {
            GameData.data.selectedLevelScene = EScenes.EasyRoom;
            SceneLoader.LoadScene(EScenes.Intro.ToString());
        });

        temp_fail.onClick.AddListener(() =>
        {
            GameData.data.selectedLevelScene = EScenes.HardRoom;
            SceneLoader.LoadScene(EScenes.Intro.ToString());
        });
        
        optionBtns[0].image.sprite = Resources.Load<Sprite>($"Sprites/snsImage0");
        optionBtns[1].image.sprite = Resources.Load<Sprite>($"Sprites/snsImage1");
        optionBtns[2].image.sprite = Resources.Load<Sprite>($"Sprites/snsImage2");
        optionBtns[3].image.sprite = Resources.Load<Sprite>($"Sprites/nonSnsImage");

        for (int i = 0; i < 3; i++)
        {
            optionBtns[i].onClick.AddListener(() =>
            {
                GameData.data.selectedLevelScene = EScenes.EasyRoom;
                SceneLoader.LoadScene(EScenes.Intro.ToString());
            });
        }
        
        optionBtns[3].onClick.AddListener(() =>
        {
            GameData.data.selectedLevelScene = EScenes.HardRoom;
            SceneLoader.LoadScene(EScenes.Intro.ToString());
        });
        
        
        remainingTime = timeLimit;
    }

    private void Update()
    {
        if (remainingTime <= 0)
        {
            GameData.data.selectedLevelScene = EScenes.EasyRoom;
            SceneLoader.LoadScene(EScenes.Intro.ToString());
        }
        remainingTime -= Time.deltaTime;
        timeText.text = $"Time : {remainingTime.ToString($"F1")} s";
    }
}
