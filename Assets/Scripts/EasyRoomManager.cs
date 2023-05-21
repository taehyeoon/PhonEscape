using System;
using System.Collections;
using System.Collections.Generic;
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
    protected override void Awake()
    {
        base.Awake();
        moniterPopup.SetActive(false);
        LoadImages();
        
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
    }

    private void UpdateRemainingTime()
    {
        GameData.data.remainingTime -= Time.deltaTime;
        GameData.data.remainingTime = Mathf.Max(0, GameData.data.remainingTime);
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
