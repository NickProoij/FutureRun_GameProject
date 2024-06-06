using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float laneWidth = 1f;
    public float laneSwitchSpeed = 10f;
    public float forwardSpeed = 3f;
    public float jumpForce = 4f;
    public LayerMask groundLayer;
    public float speedIncreaseRate = 0.1f;
    public int maxHealth = 100;
    public TMP_Text healthText;
    public float knockbackForce = 2f;
    public GameObject playerUI;
    public EndScreen endScreen;

    private int currentLane = 0;
    private float targetXPosition = 0;
    private bool isGrounded = true;
    [SerializeField] private Rigidbody rb;
    private float currentForwardSpeed;
    private int currentHealth;
    private ScoreManager scoreManager;
    [SerializeField] private Animator animator;
    private bool isKnockedBack = false;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    private bool m_jumpInput = false;
    private List<Collider> m_collisions = new List<Collider>();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        currentForwardSpeed = forwardSpeed;
        StartCoroutine(IncreaseSpeedOverTime());

        scoreManager = FindObjectOfType<ScoreManager>();

        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        // Handle lane switching
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentLane > -1)
            {
                currentLane--;
                targetXPosition = currentLane * laneWidth;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentLane < 1)
            {
                currentLane++;
                targetXPosition = currentLane * laneWidth;
            }
        }

        // Handle jumping input
        if (!m_jumpInput && Input.GetKey(KeyCode.Space) && isGrounded)
        {
            m_jumpInput = true;
        }

        // Smoothly move to the target lane position
        Vector3 targetPosition = new Vector3(targetXPosition, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, laneSwitchSpeed * Time.deltaTime);

        if (!isKnockedBack)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, currentForwardSpeed);
        }

        // Update MoveSpeed parameter for running animation
        animator.SetFloat("MoveSpeed", currentForwardSpeed);
    }

    void FixedUpdate()
    {
        animator.SetBool("Grounded", isGrounded);

        // Handle jumping
        if (isGrounded && m_jumpInput && (Time.time - m_jumpTimeStamp) >= m_minJumpInterval)
        {
            m_jumpTimeStamp = Time.time;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        isGrounded = CheckGrounded();
        m_jumpInput = false;
    }

    bool CheckGrounded()
    {
        // Check if the player is grounded using raycasts
        float rayLength = 1.1f;
        Vector3 origin = transform.position;
        Vector3[] offsets = { Vector3.left * 0.5f, Vector3.right * 0.5f, Vector3.forward * 0.5f, Vector3.back * 0.5f, Vector3.zero };

        foreach (var offset in offsets)
        {
            if (Physics.Raycast(origin + offset, Vector3.down, rayLength, groundLayer))
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            currentForwardSpeed += speedIncreaseRate;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            scoreManager.AddCoinPoints(50);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Apple"))
        {
            scoreManager.AddCollectable();
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                isGrounded = true;
            }
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                TakeDamage(obstacle.damage);
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true;
                break;
            }
        }

        if (validSurfaceNormal)
        {
            isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0)
            {
                isGrounded = false;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0)
        {
            isGrounded = false;
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            EndGame();
        }
        else
        {
            Knockback();
        }
    }

    void Knockback()
    {
        isKnockedBack = true;
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -knockbackForce);
        StartCoroutine(ResetKnockback());
    }

    IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(0.5f);
        isKnockedBack = false;
    }

    void UpdateHealthUI()
    {
        healthText.text = "Health: " + currentHealth;
    }

    void EndGame()
    {
        playerUI.SetActive(false);
        endScreen.ShowEndScreen(scoreManager.GetFinalScore(), scoreManager.coinCount);
        scoreManager.SaveScoreToLeaderboard();
        Time.timeScale = 0f;
    }
}