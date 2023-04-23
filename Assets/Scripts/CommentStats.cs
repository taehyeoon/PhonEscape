using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommentStats : MonoBehaviour
{
    private int index;
    
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text likeText;
    [SerializeField] private TMP_Text dislikeText;
    [SerializeField] private GameObject EvaluationPanel;
    [SerializeField] private Button likeBtn;
    [SerializeField] private Button dislikeBtn;

    private void Awake()
    {
        {
            float height = GameData.data.rowHeight;
            int textAreaRatio = GameData.data.textAreaRatio;
            int btnAreaRatio = GameData.data.btnAreaRatio;
            int sum = textAreaRatio + btnAreaRatio;

            messageText.gameObject.GetComponent<RectTransform>().sizeDelta
                = new Vector2(0f, height * (textAreaRatio * 1f / sum));
            EvaluationPanel.gameObject.GetComponent<RectTransform>().sizeDelta
                = new Vector2(0f, height * (btnAreaRatio * 1f / sum));
        }

        likeBtn.onClick.AddListener(() =>
        {
            GameData.data.likeList[index] += 1;
            likeText.text = GameData.data.likeList[index].ToString();
        });

        dislikeBtn.onClick.AddListener(() =>
        {
            GameData.data.dislikeList[index] += 1;
            dislikeText.text = GameData.data.dislikeList[index].ToString();
        });
    }

    public void Init(int index, string message, int like, int dislike)
    {
        this.index = index;

        messageText.text = message;
        likeText.text = like.ToString();
        dislikeText.text = dislike.ToString();
    }
}
