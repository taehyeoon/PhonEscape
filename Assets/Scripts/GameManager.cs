using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager GetInstance()
    {
        if (instance == null)
            instance = new GameManager();

        return instance;
    }


}
