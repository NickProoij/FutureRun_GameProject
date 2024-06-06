using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text multiplierText;
    public TMP_Text coinText;
    public GameObject multiplierBackground;
    public float scoreMultiplier = 1f;
    public int collectablesForMultiplier = 5;

    private float score = 0f;
    private int collectableCount = 0;
    public int coinCount = 0;

    void Start()
    {
        UpdateScoreUI();
    }

    void Update()
    {
        // Increase score based on the player's distance traveled
        score += Time.deltaTime * 10 * scoreMultiplier;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        // Update the score display
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");

        // Update the multiplier text and background if the multiplier is greater than 1
        if (scoreMultiplier > 1)
        {
            multiplierText.text = "x" + Mathf.FloorToInt(scoreMultiplier).ToString(); // Display as integer
            multiplierText.gameObject.SetActive(true);
            multiplierBackground.SetActive(true);
        }
        else
        {
            multiplierText.gameObject.SetActive(false);
            multiplierBackground.SetActive(false);
        }

        // Update the coin display
        coinText.text = coinCount.ToString();
    }

    public void AddCoinPoints(int points)
    {
        // Add points to the score based on the score multiplier
        score += points * scoreMultiplier;
        coinCount++;
        UpdateScoreUI();
    }

    public void AddCollectable()
    {
        // Increase the collectable count and update the multiplier if needed
        collectableCount++;
        if (collectableCount >= collectablesForMultiplier)
        {
            IncreaseMultiplier(1);
            collectableCount = 0;
        }
        UpdateScoreUI();
    }

    public void IncreaseMultiplier(float amount)
    {
        // Increase the score multiplier
        scoreMultiplier += amount;
        UpdateScoreUI();
    }

    public float GetFinalScore()
    {
        // Calculate the final score
        return score + (coinCount * scoreMultiplier * 2);
    }

    public void SaveScoreToLeaderboard()
    {
        // Save the score to the leaderboard
        Leaderboard.SaveScore(GetFinalScore());
    }
}