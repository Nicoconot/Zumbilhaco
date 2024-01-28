using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;
    [SerializeField] private GameObject firstPlatform;

    private Dictionary<GameObject, Platform> platTable = new();

    private GameObject queuedPlatform;
    [SerializeField] private Transform platformParent;
    
    //Game over
    [SerializeField] private TextMeshProUGUI finalScoreDisplay;
    [SerializeField] private GameObject gameOverDisplay;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var go in platforms)
        {
            platTable.Add(go, go.GetComponent<Platform>());
        }

        GlobalVariables.OnGameStateChanged += CheckGameOver;
        QueuePlatform();
        NextPlatform();
    }

    private void QueuePlatform()
    {
        if (queuedPlatform == null) queuedPlatform = firstPlatform;
        else
        {
            var compatiblePlatforms = FindCompatiblePlatforms(queuedPlatform.GetComponent<Platform>().compatibleIDs);
            int rando = Random.Range(0, compatiblePlatforms.Count);
            var randoPlatform = compatiblePlatforms[rando];
            queuedPlatform = platTable.FirstOrDefault(x => x.Value == randoPlatform).Key;
        }
    }

    public void NextPlatform()
    {
        var temp = Instantiate(queuedPlatform, platformParent);

        temp.GetComponent<Platform>().OnMovementCompleted += NextPlatform;
        
        QueuePlatform();
    }

    private List<Platform> FindCompatiblePlatforms(string[] compatibleIDs)
    {
        List<Platform> compatiblePlatforms = new();
        foreach (var id in compatibleIDs)
        {
            compatiblePlatforms.Add(platTable.Values.ToList().Find(x => x.platformID == id));
        }

        return compatiblePlatforms;
    }

    private void CheckGameOver()
    {
        if (GlobalVariables.gameState == 3)
        {
            //Game over
            finalScoreDisplay.text = "Score final: " + GlobalVariables.score;
            gameOverDisplay.SetActive(true);
        }
    }
    

    public void Reset()
    {
        SceneManager.LoadScene("Main");
    }

    private void OnDestroy()
    {
        GlobalVariables.OnGameStateChanged -= CheckGameOver;
    }
}
