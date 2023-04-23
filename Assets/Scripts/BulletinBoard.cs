using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletinBoard : MonoBehaviour
{
    [SerializeField] private RectTransform rowParentTransform;
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button postBtn;

    private float rowHeight;

    private void Awake()
    {
        rowHeight = GameData.data.rowHeight;

        rowParentTransform.sizeDelta = new Vector2(0f, rowHeight * GameData.data.commentNumber);
        rowParentTransform.anchoredPosition = new Vector2(0, 0);

        for(int i = 0; i < GameData.data.commentNumber; i++)
        {
            CreateRow(
                i,
                GameData.data.messageList[i],
                GameData.data.likeList[i],
                GameData.data.dislikeList[i]);
        }

        postBtn.onClick.AddListener(() =>
        {
            if (!string.IsNullOrEmpty(inputField.text))
            {
                AddRow(inputField.text);
                inputField.text = "";
            }
        });

    }

    private void AddRow(string message)
    {
        GameData.data.AddBulletinBoardRow(message);

        rowParentTransform.sizeDelta = new Vector2(0f, rowHeight * GameData.data.commentNumber);

        CreateRow(GameData.data.commentNumber - 1, message, 0, 0);
    }

    private void CreateRow(int index, string message, int like, int dislike)
    {
        GameObject row = Instantiate(rowPrefab, rowParentTransform);
        RectTransform rt = row.GetComponent<RectTransform>();
        CommentStats stats = row.GetComponent<CommentStats>();

        rt.sizeDelta = new Vector2(0f, rowHeight);
        rt.anchoredPosition = new Vector2(0f, -rowHeight * index);

        stats.Init(index, message, like, dislike);
    }

}


