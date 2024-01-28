using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Background : MonoBehaviour
{
    [SerializeField] private SpriteRenderer backgroundSprite1, backgroundSprite2, backgroundSprite3;   
    [SerializeField] private float speed = 10;
    private float scrollingTime => GlobalVariables.ScrollingSpeed * speed;

    private float counter = 0;

    private float scrollLength;

    private bool ready = false;

    private Vector2 currentPos;
    private Vector2 length;

    private bool isMainMenu = false;
    private bool isGameRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        scrollLength = backgroundSprite1.bounds.size.x;
        length = new Vector2(scrollLength, 0);
        
        print(scrollLength);

        isMainMenu = SceneManager.GetActiveScene().name == "Main Menu";

        GlobalVariables.OnGameStateChanged += UpdateStateBool;

        ready = true;
    }

    void UpdateStateBool()
    {
        isGameRunning = GlobalVariables.gameState == 1;
    }
    // Update is called once per frame
    void Update()
    {
        if ((isGameRunning && ready) || isMainMenu)
        {
            //If game is running
            currentPos = new Vector2(Mathf.Lerp(0, -scrollLength, counter / scrollingTime), 0);
            backgroundSprite1.transform.localPosition = currentPos;
            backgroundSprite2.transform.localPosition = currentPos + length;
            backgroundSprite3.transform.localPosition = currentPos - length;

            counter += Time.deltaTime;
        }

        if (counter >= scrollingTime) counter = 0;
    }

    private void OnDestroy()
    {
        GlobalVariables.OnGameStateChanged -= UpdateStateBool;
    }
}
