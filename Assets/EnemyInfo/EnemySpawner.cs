using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemiesToSpawn;
    public int enemiesSpawned;
    private int numAlive = 0;
    public int attemptsAllowed;
    public float enemySpawnTimerCD;
    [SerializeField]private float timer = 0;
    public float enemySpawnRadiusOffset; // how far away each enemy is spawned from each other 
    public float enemySpawnOffSet;
    private int numberOfSpawnTries = 0;
    // multiply by 2, centered at 0,0
    [Header("Boundaries")]
    public float xBoundSize;
    public float yLowerBound;
    public float yUpperBound;

    [Header("Enemy Tracker")]
    public LayerMask layerToHit;
    public int spawnThreshold; // lower than enemiesSpawned
    public List<GameObject> enemiesAlive;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = enemySpawnTimerCD;
        enemiesAlive = new List<GameObject>();
        numberOfSpawnTries = 0;
        // for now, spawn everything at the beginning
        while(numberOfSpawnTries < attemptsAllowed && enemiesSpawned < enemiesToSpawn)
        {
            Vector3 spawnPoint = transform.position + new Vector3( Random.Range(-1 * xBoundSize, xBoundSize), Random.Range(yLowerBound, yUpperBound), Random.Range(-1 * xBoundSize, xBoundSize));
            if (Physics.CheckSphere(spawnPoint, enemySpawnRadiusOffset, layerToHit) == false)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Random.rotation);
                ++enemiesSpawned;
                enemiesAlive.Add(enemy);
            }
            else
            {
                ++numberOfSpawnTries;
            }
        }  
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        numAlive = enemiesAlive.Count;
        if(timer <= 0)
        {
            TrimEnemyList();
            numAlive = enemiesAlive.Count;
            if(numAlive < spawnThreshold)
            {
                RespawnEnemies();
                numAlive = enemiesAlive.Count;
            }
            timer = enemySpawnTimerCD;
            enemiesSpawned = numAlive;
            print("respawned up to: " + enemiesSpawned);
        }
    }

    public void RespawnEnemies()
    {   
        int tries = 0;
        while(tries < attemptsAllowed && enemiesAlive.Count < enemiesToSpawn) // avoid infinite loop , either do forloop for while
        {
            Vector3 spawnPoint = transform.position + new Vector3(Random.Range(-1 * xBoundSize, xBoundSize), Random.Range(yLowerBound, yUpperBound), Random.Range(-1 * xBoundSize, xBoundSize));
            if (Physics.CheckSphere(spawnPoint, enemySpawnRadiusOffset,layerToHit) == false)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Random.rotation);
                enemiesAlive.Add(enemy);
                tries++;
            }
        }
        
    }

    private void TrimEnemyList()
    {
        for (int i = enemiesAlive.Count - 1; i >= 0; --i)
        {
            if (enemiesAlive[i] == null || !enemiesAlive[i].activeInHierarchy)
            {
                print("removed");
                enemiesAlive.RemoveAt(i);
            }
        }
    }
}
