using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EasyRoomManager : BaseRoomManager
{
    [Header("Wire Clue")]
    [SerializeField] private GameObject moniter;
    [SerializeField] private GameObject desk;
    [SerializeField] private GameObject computerBody;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject moniterPopup;
    [SerializeField] private Button backBtn;

    public Player player;
    public Light2D wallLight;

    public ActionBtn actionBtn;
    protected override void Awake()
    {
        base.Awake();
        moniterPopup.SetActive(false);
        LoadImages();

        wallLight.intensity = 1f;
        backBtn.onClick.AddListener(() =>
        {
            loadRoom();
        });
    }

    private void Update()
    {
        if (Managers.touchData.touchedObj != null)
        {
            if (Managers.touchData.touchedObj == moniter)
            {
                moniterPopup.SetActive(true);
            }
            else
            {
                if (moniterPopup.activeSelf && Managers.touchData.touchedObj.name == "outside")
                {
                    moniterPopup.SetActive(false);
                }   
            }
            Debug.Log(Managers.touchData.touchedObj.name);
        }

        UpdateRemainingTime();
        UpdateWallLight();
        UpdateActionBtnState();
        
    }

    private void UpdateActionBtnState()
    {
        GameObject frontObj = player.GetFrontObject();
        // If the object in front is a wall
        if (frontObj != null)
        {
            if(frontObj.layer != LayerMask.GetMask("Wall"))
                actionBtn.SetBtn(EBtnState.Wall, frontObj.GetComponent<Wall>().orientation);
            else
            {
                actionBtn.SetBtn(EBtnState.A, EWall.West);
            }
        }
        else
        {
            actionBtn.SetBtn(EBtnState.A, EWall.West);

        }
    }

    private void UpdateWallLight()
    {
        wallLight.intensity = (1 - GameData.data.minWallLightRange) * GameData.data.remainingTimeRatio +
                              GameData.data.minWallLightRange;
    }

    private void UpdateRemainingTime()
    {
        GameData.data.remainingTime -= Time.deltaTime;
        if (GameData.data.remainingTime < 0) GameData.data.remainingTime = 0f;
    }

    private void LoadImages()
    {
        string wireImgPath = "Sprites/Clues/";
        moniter.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(wireImgPath+moniter.name);
        desk.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(wireImgPath + desk.name);
        computerBody.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(wireImgPath+computerBody.name);
        map.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(wireImgPath + map.name);
        
        
    }
}
