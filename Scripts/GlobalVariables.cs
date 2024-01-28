using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GlobalVariables
{
    public static float ScrollingSpeed = 1.5f;
    public static int gameState = 0; //0 - Not started, 1 - Running, 2 - paused, 3 - dead, 4 - finished
    public static Player player = null;
    public static Vector3 endPoint = Vector3.zero;
    public static int score = 0;

    public static UnityAction OnGameStateChanged;

    public static void ChangeGameState(int newState)
    {
        gameState = newState;
        OnGameStateChanged?.Invoke();
    }
}
