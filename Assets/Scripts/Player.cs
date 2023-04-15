using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Component")]
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private FloatingJoystick joystick;

    [Header("Player Data")]
    [SerializeField] private float speed;
    [SerializeField][Range(0, 1)] private float joystickRangeMin; // The player moves when the stick is further than this value from the center

    [Header("Input")]
    private Vector2 prevInput;

    [Header("Animator parameter")]
    private string anim_para_up;
    private string anim_para_down;
    private string anim_para_right;
    private string anim_para_left;
    private string anim_para_stop;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim_para_up = "up";
        anim_para_down = "down";
        anim_para_right = "right";
        anim_para_left = "left";
        anim_para_stop = "stop";
    }


    private void FixedUpdate()
    {
        Vector2 curInput = GetInputDir();

        if (!Vector2.Equals(curInput, prevInput))
        {
            ChangeSprite(curInput);
        }
        Move(curInput);

        prevInput = curInput;
    }


    private void ChangeSprite(Vector2 dir)
    {
        if (dir == Vector2.up)          anim.SetTrigger(anim_para_up);
        else if (dir == Vector2.down)   anim.SetTrigger(anim_para_down);
        else if (dir == Vector2.left)   anim.SetTrigger(anim_para_left);
        else if (dir == Vector2.right)  anim.SetTrigger(anim_para_right);
        else if (dir == Vector2.zero)   anim.SetTrigger(anim_para_stop);
        else Debug.LogWarning("Inappropriate input");
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

            if (angle < 135f && angle >= 45f)           return Vector2.up;
            else if (angle < -45f && angle >= -135f)    return Vector2.down;
            else if (angle < -135f || angle >= 135f)    return Vector2.left;
            else if (angle >= -45f && angle < 45f)      return Vector2.right;
            else Debug.LogWarning("Inappropriate input");
        }
        return Vector2.zero;
    }
}
