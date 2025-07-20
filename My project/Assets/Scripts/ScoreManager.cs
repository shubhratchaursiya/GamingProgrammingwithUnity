using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton for easy access

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;       // Live Score UI (playing)
    public TextMeshProUGUI finalScoreText;  // Final Score UI (Game Over Panel)

    public int score = 0;
    private bool isScoring = true;

    void Awake()
    {
        // Singleton pattern to access this script from anywhere
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        score = 0;
        isScoring = true;
        UpdateScoreUI();

        if (finalScoreText != null)
            finalScoreText.text = ""; // Hide final score at start
    }

    void Update()
    {
        if (isScoring)
        {
            // ✅ Adds score over time (1 point per 0.1 seconds)
            score += Mathf.FloorToInt(Time.deltaTime * 10f);
            UpdateScoreUI();
        }
    }

    public void AddScore(int points)
    {
        if (!isScoring) return;
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void StopScoring()
    {
        isScoring = false;

        if (finalScoreText != null)
            finalScoreText.text = "Final Score: " + score;
    }
}

