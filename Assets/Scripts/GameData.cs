using UnityEngine;

public enum EScenes
{
    Menu,
    SelectName,
    Quiz,
    Intro,
    EasyRoom,
    HardRoom,
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

    [Header("Player Data")]
    public string playreName;
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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(data == null)
        {
            data = this;
        }

        if(introTextList.Length == 0)
        {
            Debug.LogError("The number of elements in the introTextList must be at least one");
        }
    }



}
