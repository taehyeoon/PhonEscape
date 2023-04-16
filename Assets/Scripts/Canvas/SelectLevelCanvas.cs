using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelCanvas : MonoBehaviour
{
    [SerializeField] private Button easyBtn;    
    [SerializeField] private Button hardBtn;
    [SerializeField] private TMP_InputField nameInputField;

    private void Awake()
    {
        easyBtn.onClick.AddListener(() =>
        {
            if(checkNameVaild())
                SceneLoader.LoadScene(EScenes.EasyRoom.ToString());
        });

        hardBtn.onClick.AddListener(() =>
        {
            Debug.Log("Move to HardRoomScene");
            /*
            if (checkNameVaild())
                SceneLoader.LoadScene(EScenes.HardRoom.ToString());
            */
        });
    }

    private bool checkNameVaild()
    {
        if (string.IsNullOrEmpty(nameInputField.text))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
