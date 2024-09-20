using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class engineAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] float switchTime = 0.2f;
    private SpriteRenderer spriteRenderer;
    private int i = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(SwitchSprite());
    }

    // Update is called once per frame
    IEnumerator SwitchSprite()
    {
        while (true)
        {
            spriteRenderer.sprite = sprites[i];

            i = (i + 1) % sprites.Length;

            yield return new WaitForSeconds(switchTime);
        }
    }
}
