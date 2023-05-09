using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button quitBtn;

    private void Awake()
    {
        startBtn.onClick.AddListener(() =>
        {
            Managers.audioManager.PlaySfx("btnClickClip");
            SceneLoader.LoadScene(EScenes.SelectName.ToString());
        });

        quitBtn.onClick.AddListener(() =>
        {
            Debug.Log("quit");
            Application.Quit();
        });

    }

}
