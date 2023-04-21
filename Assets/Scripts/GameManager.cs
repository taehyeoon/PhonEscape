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

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


    public static GameManager GetInstance()
    {
        if (instance == null)
            instance = new GameManager();

        return instance;
    }


}
