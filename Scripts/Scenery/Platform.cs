using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    public string platformID;
    public string[] compatibleIDs;

    public float length;
    private float movementTime;

    private float counter;

    private float yPos;

    private bool ready = true;
    private bool isGameRunning = false;

    private float endPoint, startPoint = -50;

    [HideInInspector] public bool invoked = false;



    public UnityAction OnMovementCompleted;
    private void Awake()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        movementTime = 10 / GlobalVariables.ScrollingSpeed;
        
        yPos = transform.position.y;

        if (!gameObject.CompareTag("FirstPlatform")) endPoint = GlobalVariables.endPoint.x;
        else
        {
            endPoint = -5;
        }

        startPoint = endPoint - 50;

    }

    private void Start()
    {
        GlobalVariables.OnGameStateChanged += UpdateStateBool;
        UpdateStateBool();
    }

    void UpdateStateBool()
    {
        isGameRunning = GlobalVariables.gameState == 1;
    }

    private void Update()
    {
        if (!ready) return;
        if (isGameRunning)
        {
            transform.position = new Vector2(Mathf.Lerp(endPoint, startPoint, counter / movementTime), yPos);

            counter += Time.deltaTime;
            
        }
        if (counter >= movementTime)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator CompleteMovement()
    {
        invoked = true;
        float rando = Random.Range(0.1f, 1f);

        yield return new WaitForSeconds(rando);
        
        OnMovementCompleted?.Invoke();
        
    }

    private void OnDestroy()
    {
        GlobalVariables.OnGameStateChanged -= UpdateStateBool;
    }
}
