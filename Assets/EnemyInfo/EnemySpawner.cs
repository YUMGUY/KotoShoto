using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemiesToSpawn;
    public float enemySpawnTimerCD;
    public float enemySpawnRadius; 
    public float enemySpawnOffSet;


    // Start is called before the first frame update
    void Start()
    {
        // for now, spawn everything at the beginning
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
