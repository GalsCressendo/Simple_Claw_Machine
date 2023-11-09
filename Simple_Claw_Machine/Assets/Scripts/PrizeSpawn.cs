using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeSpawn : MonoBehaviour
{
    public GameObject[] spawnPoint;
    public GameObject[] prizes;
    public int prizeCount = 30;

    private void Start()
    {
        SpawnPrizes();
    }

    private void SpawnPrizes()
    {
        for (int i = 1; i <= prizeCount; i++)
        {
            var randomPrize = prizes[Random.Range(0, prizes.Length-1)];
            var randomSpawnPoint = spawnPoint[Random.Range(0, spawnPoint.Length - 1)];
            Instantiate(randomPrize, randomSpawnPoint.transform.position, Quaternion.identity);
        }
    }
}
