using UnityEngine;

public class Entity : MonoBehaviour
{

    protected Rigidbody2D rb;
    protected Animator animator;

    protected int facingDire = 1;
    protected bool facingRight = true;

    [Header("Collision info")]
    protected bool isGrounded;
    protected bool isWallDetected;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatisGround; // 碰撞层
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatisWall; // 碰撞层

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        if (wallCheck == null)
        {
            wallCheck = transform;
        }
    }

    protected virtual void Update()
    {
        CollisionChecks();
    }

    /**
     *  检测是否地面上
     */
    protected virtual void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatisGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facingDire, whatisGround);
    }

    /**
    * 反转方向
    */
    protected virtual void Flip()
    {
        facingDire = facingDire * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


    /**
     * 角色画一个射线，用于检测是否在地面上
     */
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDire, wallCheck.position.y));
    }
}
