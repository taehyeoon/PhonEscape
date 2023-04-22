using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallCanvas : MonoBehaviour
{
    [SerializeField]
    private Button roomBtn;

    private void Awake()
    {
        roomBtn.onClick.AddListener(() =>
        {
            EasyRoomManager.loadRoom();
        });
    }
}
