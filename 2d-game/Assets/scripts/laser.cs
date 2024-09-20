using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] int direction = 1;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position += new Vector3(0, speed * direction, 0) * Time.deltaTime;
    }

}
