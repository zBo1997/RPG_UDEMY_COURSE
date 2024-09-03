using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeletion : Entity
{
    [Header("Move info")]
    [SerializeField] private float moveSpeed = 2f;

    override protected void Start()
    {
        base.Start();
    }

    override protected void Update()
    {
        base.Update();
        if (!isGrounded || isWallDetected)
        {
            Flip();
        }
        rb.velocity = new Vector2(moveSpeed * facingDire, rb.velocity.y);

    }
}
