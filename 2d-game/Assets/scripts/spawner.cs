using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] float spawnRate = 3f;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject hpPill;
    [SerializeField] GameObject laserUp;
    [SerializeField] GameManager manager;

    float xMin;
    float xMax;
    float yspawn;
    float nextSpawnTime;
    bool cheated = false;
    void Start()
    {
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0.1f,0,0)).x;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(0.9f,0,0)).x;
        yspawn = Camera.main.ViewportToWorldPoint(new Vector3(0, 1.25f, 0)).y;
        nextSpawnTime = Time.time + spawnRate;
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.Q)) {
            cheated = true;
            spawnRate = 1;
        }
        if (!cheated)
        {
            spawnRate = Mathf.Max(1, 3 - manager.score / 100);
        }
            if (Time.time >= nextSpawnTime)
        {
            updateSpawn();
            nextSpawnTime = Time.time + spawnRate;
        }
    }
    void updateSpawn() {
        float xSpawn = Random.Range(xMin, xMax);
        GameObject spawnedEnemy = Instantiate(enemy, new Vector3(xSpawn, yspawn, 0), Quaternion.identity);
        SpriteRenderer enemyRenderer = spawnedEnemy.GetComponent<SpriteRenderer>();
        enemyRenderer.sortingOrder = 1;
        enemy enemyScript = spawnedEnemy.GetComponent<enemy>();
        enemyScript.SetLaser(laser);
        enemyScript.SetPill(hpPill);
        enemyScript.SetUp(laserUp);
    }
}
