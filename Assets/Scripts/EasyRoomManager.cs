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
    [SerializeField] private Button backBtn;

    public Player player;
    
    [Header("Light")]
    public Light2D spotLight;
    public ActionBtn actionBtn;
    protected override void Awake()
    {
        base.Awake();
        
        wallGlobalLight.intensity = 1f;
        InitializedLight();
        
        backBtn.onClick.AddListener(() =>
        {
            roomGlobalLight.gameObject.SetActive(true);
            wallGlobalLight.gameObject.SetActive(false);
            loadRoom();
        });
    }

    private void Update()
    {
        UpdateRemainingTime();
        UpdateWallLight();
        UpdateActionBtnState();
        CalculateSpotlightRange();
    }

    private void InitializedLight()
    {
        spotLight.pointLightOuterRadius = GameData.data.maxSpotlightRange;
        spotLight.pointLightInnerRadius = spotLight.pointLightOuterRadius * GameData.data.innerOuterRadiusRatio;
    }
    
    private void CalculateSpotlightRange()
    {
        spotLight.pointLightOuterRadius = GameData.data.remainingTimeRatio * (GameData.data.maxSpotlightRange - GameData.data.minSpotlightRange) + GameData.data.minSpotlightRange;
        spotLight.pointLightInnerRadius = spotLight.pointLightOuterRadius * GameData.data.innerOuterRadiusRatio;
    }
    
    private void UpdateActionBtnState()
    {
        GameObject frontObj = player.GetFrontObject();
        // If the object in front is a wall
        if (frontObj != null)
        {
            Debug.Log(frontObj.layer);
            Debug.Log("Wall layer : " + LayerMask.NameToLayer("Wall"));
            if(frontObj.layer == LayerMask.NameToLayer("Wall"))
                actionBtn.SetBtn(EBtnState.Wall, frontObj);
            else if(frontObj.layer == LayerMask.NameToLayer("Trash"))
                actionBtn.SetBtn(EBtnState.Trash, frontObj);
            else if(frontObj.layer == LayerMask.NameToLayer("TrashCan"))
                actionBtn.SetBtn(EBtnState.TrashCan, frontObj);
            else
            {
                actionBtn.SetBtn(EBtnState.A, null);
            }
        }
        else
        {
            actionBtn.SetBtn(EBtnState.A, null);

        }
    }

    private void UpdateWallLight()
    {
        wallGlobalLight.intensity = (1 - GameData.data.minWallLightRange) * GameData.data.remainingTimeRatio +
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
