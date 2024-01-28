using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;

    protected float originalGravity, loweredGravity;
    
    [SerializeField] public float jumpForce = 10;
    [SerializeField] private GroundCheck groundCheck;
    
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    protected virtual void Start()
    {
        originalGravity = rb.gravityScale;
        loweredGravity = originalGravity / 15;
    }
    
    protected virtual void LowerGravity()
    {
        rb.gravityScale = loweredGravity;
    }

    protected virtual void ReturnGravity()
    {
        rb.gravityScale = originalGravity;
    }
    
    protected virtual void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
    }

    protected bool CanJump()
    {
        return Mathf.Abs(rb.velocity.y) < 0.1f;
        //return groundCheck.canJump;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Floor"))
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        
    }
}
