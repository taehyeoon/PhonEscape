//#define UNITY_EDITOR
#define ANDROID

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerSprite
{
    player_idle_up,
    player_idle_down,
    player_idle_side,
    //walk_up_1,
    //walk_up_2,
    //walk_up_3,
    //walk_down_1,
    //walk_down_2,
    //walk_down_3,
    //walk_side_1,
    //walk_side_2,
    //walk_side_3,
}

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 prevInput;
    [SerializeField] private float speed;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField][Tooltip("The player moves when the stick is further than this value from the center")]
    [Range(0, 1)] private float joystickRangeMin;

    Dictionary<EPlayerSprite, Sprite> spriteNames;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initSprites();
    }


    private void FixedUpdate()
    {
        Vector2 curInput = GetInputDir();
        if(curInput != Vector2.zero)
        {
            Move(curInput);
            ChangeSprite(curInput);
        }
    }


    private void initSprites()
    {
        spriteNames = new Dictionary<EPlayerSprite, Sprite>();

        foreach (EPlayerSprite spriteVal in Enum.GetValues(typeof(EPlayerSprite)))
        {
            string spriteAddress = $"Player/{spriteVal}";
            if (Resources.Load<Sprite>(spriteAddress) != null)
                spriteNames.Add(spriteVal, Resources.Load<Sprite>(spriteAddress));
            else
                Debug.LogWarning($"Resources folder does not contain {spriteAddress}");
        }
    }


    private void ChangeSprite(Vector2 curInput)
    {
        if (Vector2.Equals(curInput, prevInput))
            return;
        prevInput = curInput;

        if (curInput == Vector2.up)
        {
            spriteRenderer.sprite = spriteNames[EPlayerSprite.player_idle_up];
        }
        else if (curInput == Vector2.down)
        {
            spriteRenderer.sprite = spriteNames[EPlayerSprite.player_idle_down];
        }
        else if (curInput == Vector2.left)
        {
            spriteRenderer.sprite = spriteNames[EPlayerSprite.player_idle_side];
            spriteRenderer.flipX = true;
        }
        else if (curInput == Vector2.right)
        {
            spriteRenderer.sprite = spriteNames[EPlayerSprite.player_idle_side];
            spriteRenderer.flipX = false;
        }
        else
        {
            Debug.LogWarning(curInput);
            Debug.LogWarning("Inappropriate input");
        }
    }


    private void Move(Vector2 dir)
    {
        dir = dir.normalized;
        transform.Translate(dir * speed * Time.fixedDeltaTime);
    }


    private Vector2 GetInputDir()
    {
        // 조이스틱이 중심으로부터 일정 거리이상 떨어져야 input값을 입력받습니다.
        if (joystick.Direction.magnitude >= joystickRangeMin)
        {
            float angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;

            // UP
            if (angle < 135f && angle >= 45f) return Vector2.up;
            // DOWN
            else if (angle < -45f && angle >= -135f) return Vector2.down;
            // LEFT
            else if (angle < -135f || angle >= 135f) return Vector2.left;
            // RIGHT
            else if (angle >= -45f && angle < 45f) return Vector2.right;
            else
            {
                Debug.LogWarning($"angle : {angle}");
                Debug.LogWarning("Inappropriate input");
            }
        }
        return Vector2.zero;
    }
}
