using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player_controller : MonoBehaviour
{
    [Header("Lane Movement")]
    [SerializeField] Transform center_pos;
    [SerializeField] Transform left_pos;
    [SerializeField] Transform right_pos;

    int currunt_pos = 1; // 0 = Left, 1 = Center, 2 = Right
    public float side_speed = 5f;
    public float running_Speed = 10f;
    public float jump_Force = 7f;

    [Header("Components")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;

    [Header("UI Elements")]
    public int coinDistance = 0;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] GameObject gameOverPanel;

    bool isGrounded = true;
    bool isGameOver = false;
    bool gameStarted = false; // ✅ The game only starts after 1s

    float distanceTravelled = 0f;
    Vector3 startPosition;

    [Header("Sounds")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip gameOverSound;

    void Start()
    {
        isGameOver = false;
        isGrounded = true;
        gameStarted = false;

        rb.velocity = Vector3.zero;

        if (animator == null)
            animator = GetComponent<Animator>();

        UpdateCoinUI();
        if (distanceText != null)
            distanceText.text = "Distance: 0 m";

        startPosition = transform.position;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (audioSource != null && backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

        // ✅ Give 1 second before the game truly starts
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1f);
        gameStarted = true;
    }

    void FixedUpdate()
    {
        if (isGameOver) return;

        animator.SetBool("isRunning", true);

        Vector3 targetPosition = center_pos.position;
        if (currunt_pos == 0) targetPosition = left_pos.position;
        else if (currunt_pos == 2) targetPosition = right_pos.position;

        float newX = Mathf.MoveTowards(rb.position.x, targetPosition.x, side_speed * Time.fixedDeltaTime);
        Vector3 finalPosition = new Vector3(newX, rb.position.y, rb.position.z + running_Speed * Time.fixedDeltaTime);
        rb.MovePosition(finalPosition);
    }

    void Update()
    {
        if (isGameOver) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currunt_pos > 0)
        {
            currunt_pos--;
            animator.SetTrigger("LeftMove");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currunt_pos < 2)
        {
            currunt_pos++;
            animator.SetTrigger("RightMove");
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jump_Force, rb.velocity.z);
            animator.SetTrigger("Jump");
            isGrounded = false;

            if (jumpSound != null) audioSource.PlayOneShot(jumpSound);
        }

        distanceTravelled = Vector3.Distance(startPosition, transform.position);
        if (distanceText != null)
            distanceText.text = "Distance: " + Mathf.FloorToInt(distanceTravelled) + " m";
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (gameStarted && !isGameOver && collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    public void AddCoin()
    {
        if (isGameOver) return;
        coinDistance++;
        UpdateCoinUI();

        if (coinSound != null) audioSource.PlayOneShot(coinSound);
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "Coins: " + coinDistance;
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        running_Speed = 0f;
        rb.velocity = Vector3.zero;
        animator.SetBool("isRunning", false);

        if (audioSource != null)
        {
            audioSource.Stop();
            if (gameOverSound != null)
                audioSource.PlayOneShot(gameOverSound);
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log("GAME OVER! Player hit an obstacle.");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}