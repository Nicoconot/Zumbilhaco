using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class Zombie : Entity
{
    [SerializeField] private RuntimeAnimatorController[] animators;
    [SerializeField] private Sprite[] debug_spriteBases;
    [SerializeField] private uint zombieType;

    private Player player;

    private float followSpeed = 1f;

    private bool ready = false;

    private Coroutine currentCoroutine = null;


    private Animator Anim
    {
        get {
            if (!anim)
                anim = GetComponent<Animator>();
 
            return anim;
        }
    }

    private Animator anim = null;
    
    private SpriteRenderer Sr
    {
        get {
            if (!sr)
                sr = GetComponent<SpriteRenderer>();
 
            return sr;
        }
    }

    private SpriteRenderer sr = null;

    private void OnValidate()
    {
        if (animators.Length <= zombieType)
        {
            Debug.LogError("Pleasse assign animators correctly to the zombie object!");
            zombieType = (uint)animators.Length - 1;
        }

        ;
        
        if (debug_spriteBases.Length <= zombieType)
        {
            Debug.LogError("Pleasse assign debug sprites correctly to the zombie object!");
            return;
        } 
        Sr.sprite = debug_spriteBases[(int)zombieType];
        
        UpdateType();
    }

    void UpdateType()
    {
        Anim.runtimeAnimatorController = animators[(int)zombieType];
    }

    protected override void Start()
    {
        base.Start();
        player = GlobalVariables.player;
        
    }

    private void Load()
    {
        player.OnPlayerJumpPressed += StartJumpCoroutine;
        player.OnPlayerJumpReleased += ReturnGravity;
    }
    
    private void Unload()
    {
        player.OnPlayerJumpPressed -= StartJumpCoroutine;
        player.OnPlayerJumpReleased -= ReturnGravity;
    }

    public IEnumerator Spawn()
    {
        zombieType = (uint)Random.Range(0, animators.Length);
        
        UpdateType();
        
        iTween.MoveBy(gameObject, Vector3.up * 1.5f, .1f);
        yield return new WaitForSeconds(.05f);
        Load();
        iTween.MoveBy(gameObject, Vector3.down * 1.5f, .05f);
        yield return new WaitForSeconds(.03f);
        transform.SetParent(null);
        
        ready = true;
    }

    private void StartJumpCoroutine()
    {
        if (!CanJump()) return;
        currentCoroutine = StartCoroutine(JumpCoroutine());
    }

    protected override void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        LowerGravity();
    }

    private IEnumerator JumpCoroutine()
    {
        float randomWaitTime = Random.Range(0.1f, 0.3f);

        yield return new WaitForSeconds(randomWaitTime);
        
        Jump();
    }

    private void FixedUpdate()
    {
        if (!ready) return;
        rb.AddForce(Vector2.right * followSpeed);
    }

    protected override void Die()
    {
        Unload();
        GlobalVariables.score += 1;
        if(currentCoroutine != null) StopCoroutine(currentCoroutine);
        Destroy(gameObject);
    }
}
