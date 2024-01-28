using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GlobalVariables.OnGameStateChanged += EnableOrDisableText;
        EnableOrDisableText();
    }

    private void EnableOrDisableText()
    {
        text.enabled = GlobalVariables.gameState == 1;
    }

    private void Update()
    {
        text.text = "Score: " + GlobalVariables.score;
    }

    private void OnDestroy()
    {
        GlobalVariables.OnGameStateChanged -= EnableOrDisableText;
    }
}
