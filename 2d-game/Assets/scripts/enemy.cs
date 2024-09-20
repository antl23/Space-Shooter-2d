using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject hpPill;
    [SerializeField] GameObject laserUp;
    [SerializeField] GameManager manager;

    void Start()
    {
        InvokeRepeating("UpdateEvery4Second", 3f, 4f);
    }


    void Update()
    {
        transform.position -= new Vector3(0, speed, 0) * Time.deltaTime;

    }
    void UpdateEvery4Second() {
        Instantiate(laser, transform.position, Quaternion.identity);
    }
    public void SetLaser(GameObject laserPrefab)
    {
        laser = laserPrefab;
    }
    public void SetPill(GameObject hpPrefab)
    {
        hpPill = hpPrefab;
    }
    public void SetUp(GameObject powerPrefab)
    {
        laserUp = powerPrefab;
    }


    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("EnemyLaser") || collision.gameObject.CompareTag("powerUp") || collision.gameObject.CompareTag("hpUp") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("laserStopper"))
        {
            return;
        }
        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= 5f) 
        {
           
            int itemToSpawn = Random.Range(0, 2);
            if (itemToSpawn == 0)
            {
                Instantiate(hpPill, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laserUp, transform.position, Quaternion.identity);
            }
        }
        GameManager.instance.IncreaseScore(5);
        Destroy(gameObject);
        if (collision.gameObject.CompareTag("PlayerLaser"))
        {
            Destroy(collision.gameObject);
        }
    }

}
