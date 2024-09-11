using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int score = 0;
    public static GameManager instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameoverText;

    Boolean gameover = false;

    private void Awake()
    {
        instance = this; 
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Submit") && gameover) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void IncreaseScore(int amount) { 
    score += amount;
    scoreText.text = score.ToString() + "!";
    }
    public void initialGameover() { 
        gameover = true;
        gameoverText.SetActive(gameover);
        RectTransform rectTransform = scoreText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(0, 3, 0);
    }
}
