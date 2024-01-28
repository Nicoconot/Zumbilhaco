using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Entity
{
    private float startingX = -1.48f;

    private bool doJumping = false;
    private bool canJump = true;

    public UnityAction OnPlayerJumpPressed, OnPlayerJumpReleased;


    protected override void Awake()
    {
        base.Awake();
        GlobalVariables.player = this;
    }

    protected override void Start()
    {
        base.Start();
        rb.isKinematic = true;
        StartCoroutine(MoveIntoPosition());
    }

    private IEnumerator MoveIntoPosition()
    {
        iTween.MoveFrom(gameObject, new Vector3(transform.position.x, 20, 0), 2);
        yield return new WaitForSeconds(2);

        rb.isKinematic = false;
        GlobalVariables.ChangeGameState(1);
    }
    

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && CanJump())
        {
            OnPlayerJumpPressed?.Invoke();
            doJumping = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            OnPlayerJumpReleased?.Invoke();
            ReturnGravity();
        }

        if (transform.position.x < startingX)
        {
            transform.Translate(Vector2.right * 0.1f);
        }
        if (transform.position.x > startingX)
        {
            transform.Translate(Vector2.left * 0.1f);
        }
    }

    private void FixedUpdate()
    {
        if (doJumping)
        {
            Jump();
            LowerGravity();
            doJumping = false;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);

        if (col.CompareTag("Zombie"))
        {
            Die();
        }
    }

    protected override void Die()
    {
        GlobalVariables.ChangeGameState(3);
    }
}
