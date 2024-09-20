using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserStopper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PlayerLaser") || collision.gameObject.CompareTag("EnemyLaser") ) { 
        Destroy(collision.gameObject);
        }
        return;
    }
}
