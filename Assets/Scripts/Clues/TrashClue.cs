using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashClue : MonoBehaviour
{
    public SpriteRenderer trashcanRenderer;
    public Sprite[] trashCansSprites;
    public int fullnessLevel;
    private int maxFullnessLevel;
    
    public GameObject hintLight;
    
    public static Action plusFullnessLevel;
    public static Action<GameObject> getTrash;

    public GameObject clueNumber;
    public Player player;
    private void Awake()
    {
        fullnessLevel = 0;
        maxFullnessLevel = trashCansSprites.Length;
        plusFullnessLevel += PlusFullnessLevel1;
        getTrash += GetTrash1;
        SetTrashcanSprite();
        hintLight.SetActive(false);
    }


    private void Update()
    {
        // if clue complete
        if (fullnessLevel == maxFullnessLevel)
        {
            hintLight.SetActive(true);
            clueNumber.GetComponent<SpriteRenderer>().sortingLayerName = "Number";
        }
        
    }

    private void GetTrash1(GameObject trash)
    {
        trash.transform.SetParent(player.trashHolder.transform);
        trash.transform.localPosition = Vector3.zero;
        trash.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        trash.GetComponent<SpriteRenderer>().sortingOrder = -1;
        
        if (trash.GetComponent<Collider2D>() != null)
        {
            Destroy(trash.GetComponent<Collider2D>());
        }
    }
    

    public void PlusFullnessLevel1()
    {
        if (fullnessLevel + 1 < maxFullnessLevel)
        {
            fullnessLevel++;
        }
        else
        {
            fullnessLevel = maxFullnessLevel;
        }

        SetTrashcanSprite();
    }

    private void SetTrashcanSprite()
    {
        trashcanRenderer.sprite = trashCansSprites[fullnessLevel];
    }
}
