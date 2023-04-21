using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectNameCanvas : MonoBehaviour
{
    [SerializeField] private Button nextBtn;
    [SerializeField] private TMP_InputField nameInputField;

    private void Awake()
    {
        nextBtn.onClick.AddListener(() =>
        {
            if (checkNameVaild())
            {
                GameData.data.playreName = nameInputField.text;
                SceneLoader.LoadScene(EScenes.Quiz.ToString());
                AndroidToast.I.ShowToastMessage("올바른 이름입니다.");
            }
            else
            {
                AndroidToast.I.ShowToastMessage("올바른 이름을 입력하세요.");
            }

        });
    }

    private void Start()
    {
        nameInputField.Select();
        nameInputField.ActivateInputField();
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
