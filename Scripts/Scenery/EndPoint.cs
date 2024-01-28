using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void Awake()
    {
        GlobalVariables.endPoint = transform.position;
    }
}
