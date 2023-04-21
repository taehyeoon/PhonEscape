using UnityEngine;
using UnityEngine.UI;

public class QuizCanvas : MonoBehaviour
{
    public Button temp_success;        
    public Button temp_fail;

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
    }
}
