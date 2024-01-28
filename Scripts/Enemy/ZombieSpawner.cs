using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    private bool spawned = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnCollider"))
        {
            
            float spawnChance = Random.Range(0, 10) * GlobalVariables.ScrollingSpeed;

            if (spawnChance >= 4) StartCoroutine(SpawnZombie());
        }
    }

    private IEnumerator SpawnZombie()
    {
        if (spawned) yield break;
        yield return null;
        spawned = true;
        var temp = Instantiate(zombiePrefab, transform, true).GetComponent<Zombie>();
        temp.transform.position = transform.position;
        StartCoroutine(temp.Spawn());
    }
}
