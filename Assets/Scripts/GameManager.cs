using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EScenes
{
    Menu,
    SelectLevel,
    EasyRoom,
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
