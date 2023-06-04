using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EasyRoomManager : BaseRoomManager
{
    [Header("Wire Clue")]
    [SerializeField] private GameObject moniter;
    [SerializeField] private GameObject desk;
    [SerializeField] private GameObject computerBody;
    [SerializeField] private GameObject map;
    [SerializeField] private Button backBtn;

    [Header("Light")]
    public Light2D spotLight;
    public ActionBtn actionBtn;

    public static Action dropAction;

    public DialogManager dialogManager;

    public String[] storyLine;

    public bool hasHammer;

    public Fade fadeBlack;
    public Fade fadeWhite;
    
    
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

        dropAction += DropTrash;
        dropAction += FillTrashcan;
        dialogManager.ShowDialog("You're stuck in an unidentified room.\nTake the quizzes all over this room and escape.");
        hasHammer = false;
    }
    
    private void Update()
    {
        UpdateRemainingTime();
        UpdateWallLight();
        UpdateActionBtnState();
        CalculateSpotlightRange();

        if (GameData.data.remainingTime <= 0)
        {
            fadeBlack.gameObject.SetActive(true);
            fadeBlack.stopOut = false;
            Invoke(nameof(GotoGameOver), 2); 
        }
    }

    void GotoGameOver()
    {
        SceneLoader.LoadScene(EScenes.GameOver.ToString());
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
            // Set Action Button state
            if(frontObj.layer == LayerMask.NameToLayer("Wall"))
                actionBtn.SetBtn(EBtnState.Wall, frontObj);
            else if(frontObj.layer == LayerMask.NameToLayer("Trash"))
                actionBtn.SetBtn(EBtnState.Trash, frontObj);
            else if(frontObj.layer == LayerMask.NameToLayer("TrashCan"))
                actionBtn.SetBtn(EBtnState.TrashCan, frontObj);
            else
                actionBtn.SetBtn(EBtnState.A, null);
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
    
    // 플레이어 손 비우기
    // 쓰레기통 채우기
    private void DropTrash()
    {
        player.DropTrash();
    }

    private void FillTrashcan()
    {
        if(player.trashHolder.childCount > 0)
            TrashClue.plusFullnessLevel();
    }

    
}
