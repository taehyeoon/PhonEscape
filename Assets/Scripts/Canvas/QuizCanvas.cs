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
            SceneLoader.LoadScene(EScenes.EasyRoom.ToString());
        });

        temp_fail.onClick.AddListener(() =>
        {
            AndroidToast.I.ShowToastMessage("hard mode enter", AndroidToast.ToastLength.Short);
        });
    }
}
