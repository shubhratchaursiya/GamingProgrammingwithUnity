using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance; // ✅ Singleton to call from anywhere

    [Header("UI Elements")]
    [SerializeField] GameObject gameOverPanel;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false); // ✅ Hide panel at start
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0f; // ✅ Stop the game
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log("GAME OVER! Player hit an obstacle.");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // ✅ Resume game before restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

