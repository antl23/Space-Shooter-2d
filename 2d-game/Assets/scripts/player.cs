using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] GameObject laser;
    [SerializeField] GameManager manager;
    [SerializeField] GameObject superLaser;
    [SerializeField] GameObject laserUp;
    [SerializeField] GameObject laserUp2;
    [SerializeField] GameObject laserUp3;
    [SerializeField] GameObject laserUp4;
    [SerializeField] float superLaserCooldown = 10f;
    Boolean ready = true;
    Boolean first = true;
    private float cooldown = 0.5f;
    private Rigidbody2D rb;
    float xMin;
    float xMax;
    void Start()
    {
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0, 0)).x;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0, 0)).x;
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {   
        float position = 0f;

        if (manager.power == 2) {
            laser = laserUp;
        }
        if (manager.power == 3) {
            laser = laserUp2;
        }
        if (manager.power == 4)
        {
            laser = laserUp3;

        }
        if (manager.power == 5)
        {
            laser = laserUp4;
        }

        if (Input.GetKey(KeyCode.A) && transform.position.x > xMin) {
            position = -1f;
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < xMax)
        {
            position = 1f;
        }
        rb.velocity = new Vector2(position * speed, rb.velocity.y);
        if (ready && Input.GetButton("fireLaser"))
        {
            StartCoroutine(FireLaser());
        }
        if (first && manager.power >= 3) { 
            first = false;
            manager.blastReady();
        }
        if (Input.GetKey(KeyCode.R))
        {
            superLaserCooldown = 1f;
        }
        if (manager.blast && manager.power >= 3 && (Input.GetButtonDown("blast") || Input.GetKey(KeyCode.E))) {
            Instantiate(superLaser, transform.position, Quaternion.identity);
            StartCoroutine(SuperLaserCooldown());
        }
    }
    IEnumerator SuperLaserCooldown()
    {
        manager.blastNotReady();
        yield return new WaitForSeconds(superLaserCooldown);
        manager.blastReady();
    }
    IEnumerator FireLaser()
    {
        ready = false;
        Instantiate(laser, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(cooldown);
        ready = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("PlayerLaser") || collision.gameObject.CompareTag("SuperLaser"))
        {
            return;
        }
        else if (collision.gameObject.CompareTag("powerUp"))
        {
            manager.IncreasePower();
            Destroy(collision.gameObject);
            return;
        }
        else if (collision.gameObject.CompareTag("hpUp"))
        {
            manager.IncreaseHp();
            Destroy(collision.gameObject);
            return;
        }
        
        manager.DecreaseHp();
        Destroy(collision.gameObject);
        if (manager.hp == 0)
        {
            GameManager.instance.initialGameover();
            Destroy(gameObject);
            
        }
    }

}
