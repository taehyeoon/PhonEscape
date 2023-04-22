using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private EWall orientation;
    [SerializeField] private ELevel level;
    private string wallName;


    private void Awake()
    {
        wallName = $"{level}{orientation}Wall";
    }

    public string GetWallName()
    {
        return wallName;
    }
}
