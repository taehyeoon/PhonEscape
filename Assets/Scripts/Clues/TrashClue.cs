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

    public static Action PlusFullnessLevel;
    public static Action<GameObject> GetTrash;

    public Player player;
    private void Awake()
    {
        fullnessLevel = 0;
        maxFullnessLevel = trashCansSprites.Length;
        PlusFullnessLevel += PlusFullnessLevel1;
        GetTrash += GetTrash1;
        SetTrashcanSprite();
        
    }

    private void GetTrash1(GameObject trash)
    {
        trash.transform.SetParent(player.trashHolder.transform);
        trash.transform.localPosition = Vector3.zero;
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