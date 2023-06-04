using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum EScenes
{
    Menu,
    SelectName,
    Quiz,
    Intro,
    Outro,
    EasyRoom,
    HardRoom,
    GameOver,
}

public enum EWall
{
    North,
    South,
    West,
    East,
}

public enum ELevel
{
    Easy,
    Hard,
}

public class GameData : MonoBehaviour
{
    public static GameData data;

    [FormerlySerializedAs("playreName")] [Header("Player Data")]
    public string playerName;
    [Tooltip("Player speed")]
    [Range(0, 15)] public float speed;
    [Range(0, 5)] public float distanceToWall;
    [Range(0, 5)] public float scanRange;
    [Tooltip("The player moves when the stick is further than this value from the center")]
    [Range(0, 1)] public float joystickRangeMin;

    [Header("SelectName Scene")]
    public EScenes selectedLevelScene;

    [Header("Intro Scene")]
    public string[] introTextList;
    public string hintText;
    [Tooltip("The hint phrase appears slowly for a set amount of time, and then slowly disappears.")]
    [Range(0.1f, 5f)] public float hintFadeInterval;
    [Tooltip("Indicates the time when the text appears")]
    [Range(0.1f, 5f)] public float textFadeInterval;

    [Header("BulletinBoard")]
    public float rowHeight;
    public int textAreaRatio;
    public int btnAreaRatio;
    public List<string> messageList;
    public List<int> likeList;
    public List<int> dislikeList;
    [HideInInspector] public int commentNumber;

    [Header("GameData")] 
    [Tooltip("The unit is seconds")]
    public float escapeTime;
    public float remainingTime;
    [HideInInspector]
    public float remainingTimeRatio; 
    
    [Header("Light Player")]
    public float maxSpotlightRange;
    public float minSpotlightRange;
    public float innerOuterRadiusRatio;

    [Header("Light Wall")] 
    public float maxWallLightRange; 
    public float minWallLightRange; 
    
    private void Awake()
    {
        if(data == null)
        {
            data = this;
        }

        if (!CheckAllDataValid())
        {
            Debug.LogError("Certain data formats are not valid");
        }

        remainingTime = escapeTime;
    }

    private void Update()
    {
        remainingTimeRatio = remainingTime / escapeTime;
    }

    private bool CheckAllDataValid()
    {

        if (introTextList.Length == 0)
        {
            Debug.LogError("The number of elements in the introTextList must be at least one");
            return false;
        }

        if (rowHeight < 100f)
        {
            Debug.LogError("rowHeight must be at least 100");
            return false;
        }

        if (textAreaRatio == 0 || btnAreaRatio == 0)
        {
            Debug.LogError("textAreaRatio and btnAreaRatio must be at least 1");
            return false;
        }

        if (!(messageList.Count == likeList.Count && likeList.Count == dislikeList.Count))
        {
            Debug.LogError("[BulletinBoard] The number of messages, likes, and dislikes The numbers must all be the same.");
            return false;
        }
        else
        {
            commentNumber = messageList.Count;
        }

        return true;
    }

    public void AddBulletinBoardRow(string message)
    {
        commentNumber += 1;
        messageList.Add(message);
        likeList.Add(0);
        dislikeList.Add(0);
    }
}
