using UnityEngine;

public class palyer : MonoBehaviour
{
    private float xInput;

    private int facingDire = 1;

    private bool facingRight = true;

    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float jumpForce = 10f;
    
    [Header("Dash info")]

    [SerializeField] private float dashSpeed;

    [SerializeField] private float dashDuration;

    [SerializeField] private float dashCooldown;

    private float dashTime;

    private float dashCooldownTimer;

    [Header("Collision info")]

    [SerializeField] private float groundCheckDistance;

    [SerializeField] private LayerMask whatisGround; // 碰撞层

    private Animator animator;

    private bool isGrounded;

    public Rigidbody2D rb;

    void Start()
    {
        //只会被调用一次
        rb = GetComponent<Rigidbody2D>();
        // 获取动画器组件
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        MoveMent();
        CheckInput();
        CollisionChecks();

        // 控制冲刺
        dashTime -= Time.deltaTime;
        // 控制冷却时间
        dashCooldownTimer -= Time.deltaTime;

        FilController();
        AnimatorControoler();
    }


    /**
     * 动画控制
     */
    private void AnimatorControoler()
    {
        //是否在移动
        bool isMoving = rb.velocity.x != 0;
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isGrounded", isGrounded);
        //shift 冲刺
        animator.SetBool("isDashing", dashTime > 0);
    }

    /**
     * 跳跃
     */
    private void Jump (){
        if (isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    /**
     * 获取输入
     */
    private void CheckInput()
    {
        xInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            DashAbility();
        }
    }
    /**
     * 移动
     */
    private void MoveMent()
    {
        if (dashTime > 0)
        {
            rb.velocity = new Vector2(dashSpeed * xInput, 0);
        } 
        else 
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
    }

    /**
     * 反转方向
    */
    private void Flip()
    {
        facingDire = facingDire * -1;
        facingRight =!facingRight;
        transform.Rotate(0,180,0);
    }

    /**
     * 角色转头
     */
    private void FilController()
    {
        if(rb.velocity.x > 0 && !facingRight){
            Flip();
        }else if(rb.velocity.x < 0 && facingRight){
            Flip();
        }
    }


    /**
     * 角色画一个射线，用于检测是否在地面上
     */
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }

    /**
     *  检测是否地面上
     */
    private void CollisionChecks()
    {
       isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatisGround);
    }

    /**
     *    冲刺
     */
    private void DashAbility()
    {
        // 控制冲刺
    if (dashCooldownTimer < 0){
        dashTime = dashDuration;
        dashCooldownTimer = dashCooldown;
    }
    if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer < 0)
            {
                dashTime = dashDuration;
                dashCooldownTimer = dashCooldown;
            }
    }
         
}
