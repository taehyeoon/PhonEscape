using UnityEngine;

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

    [HideInInspector]
    public EWall scannedWall;

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
    public void GetFrontObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, curDir, scanRange, LayerMask.GetMask("Wall"));

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.GetComponent<Wall>().GetWallName());
            scannedWall = hit.collider.GetComponent<Wall>().orientation;
            EasyRoomCanvas.showWallBtn();
        }
        else
        {
            Debug.Log("Null");
            EasyRoomCanvas.hideWallBtn();
        }
        return;
    }
}
