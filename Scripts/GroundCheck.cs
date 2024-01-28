using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool canJump = false;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Platform") || col.CompareTag("FirstPlatform"))
        {
            canJump = true;
        }
        else canJump = false;
    }
}
