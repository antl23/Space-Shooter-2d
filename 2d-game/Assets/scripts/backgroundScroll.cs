using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScroll : MonoBehaviour
{
    [SerializeField] GameObject bg1;
    [SerializeField] GameObject bg2;
    [SerializeField] float speed = 2f;
    private float bgHeight;
    private Vector3 bg1StartPos;
    private Vector3 bg2StartPos;
    void Start()
    {
        bg1StartPos = new Vector3(2.2269f, -0.21133f, 0);
        bg2StartPos = new Vector3(2.2269f, 20.2f, 0);

        bgHeight = Mathf.Abs(bg2StartPos.y - bg1StartPos.y);
    }

    void Update()
    {
        
        bg1.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        bg2.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        
        if (bg1.transform.position.y <= bg1StartPos.y - bgHeight)
        {
            bg1.transform.position = new Vector3(bg2StartPos.x, bg2StartPos.y, bg2StartPos.z);
        }

         
        if (bg2.transform.position.y <= bg1StartPos.y - bgHeight)
        {
            bg2.transform.position = new Vector3(bg2StartPos.x, bg2StartPos.y, bg2StartPos.z);
        }
    }
}
