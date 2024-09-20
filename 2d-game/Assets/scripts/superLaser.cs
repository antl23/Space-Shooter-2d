using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class superLaser : MonoBehaviour
{
    [SerializeField]  Sprite sparkSprite; 
    [SerializeField]  Sprite laserSprite;
    [SerializeField]  BoxCollider2D laserCollider;
    [SerializeField]  Light2D laserLight;

    private float sparkDuration = 0.3f;
    private float laserDuration = 1f;
    private float LaserSpeed = 100f;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private Light2D LightObject;
    void Start()
    {
        Debug.Log("initiated");
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        StartCoroutine(AnimateSuperLaser());
    }


    IEnumerator AnimateSuperLaser() {
        Debug.Log("start");
        spriteRenderer.sprite = sparkSprite;
        float elapsedTime = 0f;
        while (elapsedTime < sparkDuration)
        {
            Debug.Log("phase1");
            transform.localScale += new Vector3(0, LaserSpeed * Time.deltaTime, 0);
            transform.position += new Vector3(0, 8 * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;
        spriteRenderer.sprite = laserSprite;
        transform.position += new Vector3(0, 12, 0);
        transform.localScale = new Vector3(originalScale.x, 50, 0);
        Vector3 laserLightOffset = new Vector3(-3.8f, 0, 0);
        LightObject = Instantiate(laserLight, transform.position + laserLightOffset, Quaternion.identity);
        while (laserDuration > elapsedTime)
        {   
            
            
            if (transform.localScale.x < 15)
            {

                transform.localScale += new Vector3(LaserSpeed * Time.deltaTime, 0, 0);
                laserCollider.size = new Vector3(LaserSpeed * Time.deltaTime, 20, 0);
                elapsedTime += Time.deltaTime;
            }
            else {
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }
        Destroy(LightObject);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyLaser") || collision.gameObject.CompareTag("PlayerLaser"))
        {
            Destroy(collision.gameObject);
        }
    }
}
