using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData data;

    public string playreName;
    [Range(0, 15)] public float speed;
    [Range(0, 5)] public float distanceToWall;
    [Range(0, 5)] public float scanRange;
    [Range(0, 1)] public float joystickRangeMin; // The player moves when the stick is further than this value from the center


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(data == null)
        {
            data = this;
        }
    }



}
