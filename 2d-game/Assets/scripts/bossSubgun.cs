using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSubgun : MonoBehaviour
{
    [SerializeField] GameObject laser;
    [SerializeField] float fireRate;
    void Start()
    {
        InvokeRepeating("UpdateEverySecond", 1f, fireRate);
    }
    public void SetLaser(GameObject laserPrefab)
    {
        laser = laserPrefab;
    }
    void UpdateEverySecond()
    {
        Instantiate(laser, transform.position, Quaternion.identity);
    }
}
