using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnd : MonoBehaviour
{
    [SerializeField] private Platform platform;
    private bool invoked = false;

    private void Awake()
    {
        if (!platform) platform = transform.parent.GetComponent<Platform>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EndCollider"))
        {
            if (!platform.invoked && !invoked)
            {
                invoked = true;
                StartCoroutine(platform.CompleteMovement());
            }
        }
    }
}
