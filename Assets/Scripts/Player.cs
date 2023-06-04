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

    [Header("trash place holder")]
    public Transform trashHolder;

    private static readonly int Left = Animator.StringToHash("left");
    private static readonly int Stop = Animator.StringToHash("stop");

    // public DialogManager dm;
    public EasyRoomManager easyRoomManager;
    public bool isTrashDialogShow;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        speed = GameData.data.speed;
        scanRange = GameData.data.scanRange;
        joystickRangeMin = GameData.data.joystickRangeMin;
        isTrashDialogShow = false;

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
            anim.SetTrigger(Left);
        }
        else if (dir == Vector2.down)
        {
            anim.SetTrigger(Left);
        }
        else if (dir == Vector2.left)
        {
            anim.SetTrigger(Left);
            GetComponent<SpriteRenderer>().flipX = false;
            // trashHolder = left;
            var position = trashHolder.localPosition;
            position = new Vector3(-Mathf.Abs(position.x), position.y, position.z);
            trashHolder.localPosition = position;
        }
        else if (dir == Vector2.right)
        {
            anim.SetTrigger(Left);
            GetComponent<SpriteRenderer>().flipX = true;
            var position = trashHolder.localPosition;
            position = new Vector3(Mathf.Abs(position.x), position.y, position.z);
            trashHolder.localPosition = position;

        }
        else if (dir == Vector2.zero)
        {
            anim.SetTrigger(Stop);
        }
        else
        {
            Debug.LogWarning("Inappropriate input : " + dir);
        }

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

    public void DropTrash()
    {
        if (trashHolder.childCount > 0)
        {
            Transform holdingTrash = trashHolder.transform.GetChild(0);
            Destroy(holdingTrash.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "trashes")
        {
            if (!isTrashDialogShow)
            {
                easyRoomManager.dialogManager.ShowDialog("It stinks. \nLet's find a place to throw away trash.");
                isTrashDialogShow = true;
            }
        }
    }
}
