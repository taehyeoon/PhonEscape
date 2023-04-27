using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private Button wallBtn;
    [SerializeField]
    private Button actionBtn;
    public static System.Action showWallBtn;
    public static System.Action hideWallBtn;
	
    [SerializeField]
    private Player playerScript;

    private void Awake()
    {
        wallBtn.gameObject.SetActive(false);
        wallBtn.onClick.AddListener(() =>
        {
            EasyRoomManager.loadWall(playerScript.scannedWall);
        });


        showWallBtn = () =>
        {
            wallBtn.gameObject.SetActive(true);
        };
        hideWallBtn = () =>
        {
            wallBtn.gameObject.SetActive(false);
        };


    }
}
