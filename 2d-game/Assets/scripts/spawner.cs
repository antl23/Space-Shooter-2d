using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] float spawnRate = 5f;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject laser;

    float xMin;
    float xMax;
    float yspawn;
    
    void Start()
    {
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0.1f,0,0)).x;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(0.9f,0,0)).x;
        yspawn = Camera.main.ViewportToWorldPoint(new Vector3(0, 1.25f, 0)).y;
        InvokeRepeating("updateSpawn",0,spawnRate);
    }

    
    void Update()
    {
        
    }
    void updateSpawn() {
        float xSpawn = Random.Range(xMin, xMax);
        GameObject spawnedEnemy = Instantiate(enemy, new Vector3(xSpawn, yspawn, 0), Quaternion.identity);
        SpriteRenderer enemyRenderer = spawnedEnemy.GetComponent<SpriteRenderer>();
        enemyRenderer.sortingOrder = 1;
        enemy enemyScript = spawnedEnemy.GetComponent<enemy>();
        enemyScript.SetLaser(laser);
    }
}
