using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject laser;
    [SerializeField] GameManager manager;

    void Start()
    {
        InvokeRepeating("UpdateEvery2Second", 3f, 2f);
    }

    
    void Update()
    {
        transform.position -= new Vector3 (0, speed, 0) * Time.deltaTime;

    }
    void UpdateEvery2Second() {
        Instantiate(laser, transform.position, Quaternion.identity);
    }
    public void SetLaser(GameObject laserPrefab)
    {
        laser = laserPrefab;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.gameObject.CompareTag("EnemyLaser"))
        {
            return;
        }
        GameManager.instance.IncreaseScore(5);
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }

}
