using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int hp = 1;
    public int power = 1;
    public Boolean blast = false;
    String blastString = "Not Ready";
    public static GameManager instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameoverText;
    [SerializeField] TextMeshProUGUI statText;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject player;

    Boolean gameover = false;
    Boolean start = false;

    private void Awake()
    {
        instance = this;
        
    }
    void Start()
    {
        start = true;
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Submit") && gameover) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetButtonDown("Submit") && start)
        {
            startText.SetActive(false);
            scoreText.gameObject.SetActive(true);
            statText.gameObject.SetActive(true);
            player.SetActive(true);
            spawner.SetActive(true);
            StartCoroutine(MovePlayerToPosition(new Vector3(0, -6.53f, 0), 1f));
        }
    }
    IEnumerator MovePlayerToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = player.transform.position;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, t / duration);
            yield return null;
        }
        player.transform.position = targetPosition;
    }
    public void IncreaseScore(int amount) { 
    score += amount;
    scoreText.text = score.ToString() + "!";
    }
    public void IncreaseHp()
    {
        if (hp < 3)
        {
            hp++;
            statText.text = "HP: " + hp.ToString() + "\tPower: " + power.ToString() + "\t\t        Blast: " + blastString;
        }
    }
    public void DecreaseHp()
    {
            hp--;
            statText.text = "HP: " + hp.ToString() + "\tPower: " + power.ToString() + "\t\t        Blast: " + blastString; 
    }
    public void IncreasePower()
    {
        if (power < 5)
        {
            power++;
            statText.text = "HP: " + hp.ToString() + "\tPower: " + power.ToString() + "\t\t        Blast: " + blastString;
        }
    }
    public void blastReady()
    {
        blast = true;
        blastString = "Ready";
        statText.text = "HP: " + hp.ToString() + "\tPower: " + power.ToString() + "\t\t        Blast: " + blastString;
    }
    public void blastNotReady() {
        blast = false;
        blastString = "Not Ready";
        statText.text = "HP: " + hp.ToString() + "\tPower: " + power.ToString() + "\t\t        Blast: " + blastString;
    }

    public void initialGameover() { 
        gameover = true;
        gameoverText.SetActive(gameover);
        RectTransform rectTransform = scoreText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(0, 3, 0);
    }
}
