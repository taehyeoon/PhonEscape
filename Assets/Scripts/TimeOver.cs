using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOver : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (GameData.data.remainingTime <= 0)
        {
            SceneLoader.LoadScene("Timeout");
        }
    }
}
