using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] GameObject laser;
    [SerializeField] GameManager manager;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float position = 0f;

        if (Input.GetKey(KeyCode.A)) {
            position = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            position = 1f;
        }
        rb.velocity = new Vector2(position * speed, rb.velocity.y);
        if (Input.GetButtonDown("fireLaser")) {
            Instantiate(laser, transform.position, Quaternion.identity);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("PlayerLaser"))
        {
            return;
        }
            GameManager.instance.initialGameover();
            Destroy(gameObject);
            Destroy(collision.gameObject);
    }

}
