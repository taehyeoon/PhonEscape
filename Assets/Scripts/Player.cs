using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [Header("Component")]
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private FloatingJoystick joystick;

    [Header("Player Data")]
    private float speed;
    private float scanRange;
    private float joystickRangeMin;

    [Header("Info")]
    private Vector2 prevInput;
    private Vector3 curDir;

    [Header("Animator parameter")]
    private string anim_para_up;
    private string anim_para_down;
    private string anim_para_right;
    private string anim_para_left;
    private string anim_para_stop;

    [Header("trash place holder")]
    public Transform trashHolder;
    public Transform left;
    public Transform right;
    public Transform up;
    public Transform down;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        speed = GameData.data.speed;
        scanRange = GameData.data.scanRange;
        joystickRangeMin = GameData.data.joystickRangeMin;

        anim_para_up = "up";
        anim_para_down = "down";
        anim_para_right = "right";
        anim_para_left = "left";
        anim_para_stop = "stop";
    }



    private void Update()
    {
        GetFrontObject();
    }

    private void FixedUpdate()
    {
        Vector2 curInput = GetInputDir();

        if (!Equals(curInput, prevInput))
        {
            ChangeAnimation(curInput);
        }

        Move(curInput);

        SetCurDir(curInput);

        prevInput = curInput;

        // Debug
        Debug.DrawRay(transform.position, curDir * scanRange, Color.yellow);
    }


    private void SetCurDir(Vector2 dir)
    {
        if (dir == Vector2.up)          curDir = Vector3.up;
        else if (dir == Vector2.down)   curDir = Vector3.down;
        else if (dir == Vector2.left)   curDir = Vector3.left;
        else if (dir == Vector2.right)  curDir = Vector3.right;
        else if (dir == Vector2.zero) return;
        else Debug.LogWarning("Inappropriate input");
    }


    private void ChangeAnimation(Vector2 dir)
    {
        if (dir == Vector2.up)
        {
            anim.SetTrigger(anim_para_left);
            trashHolder = up;
        }
        else if (dir == Vector2.down)
        {
            anim.SetTrigger(anim_para_left);
            trashHolder = down;
            
        }
        else if (dir == Vector2.left)
        {
            anim.SetTrigger(anim_para_left);
            GetComponent<SpriteRenderer>().flipX = false;
            trashHolder = left;


        }
        else if (dir == Vector2.right)
        {
            anim.SetTrigger(anim_para_left);
            GetComponent<SpriteRenderer>().flipX = true;
            trashHolder = right;

        }
        else if (dir == Vector2.zero)   anim.SetTrigger(anim_para_stop);
        else
        {
            Debug.LogWarning("Inappropriate input : " + dir);
        }

        trashHolder.position = Vector3.zero;
        
    }


    private void Move(Vector2 dir)
    {
        dir = dir.normalized;
        rb.velocity = dir * speed;
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


    // not use
    public GameObject GetFrontObject()
    {
        int layer1 = 1 << LayerMask.NameToLayer("Wall");
        int layer2 = 1 << LayerMask.NameToLayer("Trash");
        int layer3 = 1 << LayerMask.NameToLayer("TrashCan");

        int layermask = layer1 | layer2 | layer3;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, curDir, scanRange, layermask);

        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}
