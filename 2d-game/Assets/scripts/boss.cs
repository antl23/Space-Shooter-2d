using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class boss : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] GameObject shed;
    [SerializeField] GameManager manager;
    [SerializeField] GameObject gun1;
    [SerializeField] GameObject gun2;
    [SerializeField] GameObject explosion;
    [SerializeField] BoxCollider2D box;
    [SerializeField] int health = 300;

    private bool movingRight = true;
    private bool entered = false;
    private bool shedChildren = false;
    private bool dying = false;

    void Update()
    {
        if (health < 0 && !dying) {
        dying = true;
            Destroy(gun1);
            Destroy(gun2);
            TriggerExplosion(3);
        manager.IncreaseScore(1000);
        Destroy(gameObject, 3f);
        }
        if (manager.score >= 300) {
            StartCoroutine(MoveTowardsTargetY(6.61f, 1f));
        }
        if (health <= 100 && !shedChildren)
        {
            Vector2 currentSize = box.size;
            currentSize.x -= 2.20f;
            box.size = currentSize;

            TriggerExplosion(1);
            ShedChildren();
            shedChildren = true;
            speed = 4f;
            gun1.SetActive(true);
            gun2.SetActive(true);
        }
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        float screenWidth = Screen.width;
        float leftBound = 0.2f * screenWidth;
        float rightBound = 0.8f * screenWidth;

        if (entered && !dying)
        {
            if (screenPosition.x <= leftBound)
            {
                movingRight = true;
            }
            else if (screenPosition.x >= rightBound)
            {
                movingRight = false;
            }

            if (movingRight)
            {
                transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            }
            else
            {
                transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
            }
        }
    }
    IEnumerator MoveTowardsTargetY(float targetY, float duration)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);

        float lowerBound = 6.56f;
        float upperBound = 6.66f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            if (transform.position.y >= lowerBound && transform.position.y <= upperBound)
            {
                transform.position = targetPosition;
                entered = true; 
                yield break;
            }

            transform.position = Vector3.Lerp(startPosition, targetPosition, t / duration);
            yield return null;
        }
        transform.position = targetPosition;
        entered = true;
    }

    void ShedChildren()
    {
        shed.transform.Rotate(30f, 0, 0);
        foreach (Transform child in shed.transform)
        {
            StartCoroutine(FallAndDestroy(child.gameObject));
        }
    }
    IEnumerator FallAndDestroy(GameObject child)
    {
        float fallDuration = 1f;
        float fallSpeed = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fallDuration)
        {
            child.transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(child);
    }
    public void TriggerExplosion(float duration)
    {
        StartCoroutine(explode(duration));
    }
    IEnumerator explode(float duration) { 
    explosion.SetActive(true);
    yield return new WaitForSeconds(duration);
    explosion.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (entered)
        {
            if (collision.gameObject.CompareTag("PlayerLaser"))
            {
                health -= manager.power;
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("SuperLaser"))
            {
                health -= manager.power * 3;
            }
        }
    }
}
