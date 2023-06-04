using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button quitBtn;

    private void Awake()
    {
        restartBtn.onClick.AddListener(() =>
        {
            Managers.audioManager.PlaySfx("btnClickClip");
            Debug.Log("clicked");
            SceneLoader.LoadScene("Menu");
        });

        quitBtn.onClick.AddListener(() =>
        {
            Debug.Log("quit");
            Application.Quit();
        });

    }

}
