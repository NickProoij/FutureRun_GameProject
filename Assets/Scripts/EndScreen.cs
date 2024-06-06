using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public GameObject endScreen;
    public TMP_Text scoreText;
    public TMP_Text coinText;

    private void Start()
    {
        // Make sure the end screen is initially hidden
        endScreen.SetActive(false);
    }

    public void ShowEndScreen(float score, int coins)
    {
        // Show the end screen
        endScreen.SetActive(true);
        // Display the final score
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString("D5");
        // Display the number of coins
        coinText.text = "Coins: " + coins.ToString();
        // Pause the game
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        // Resume the game
        Time.timeScale = 1;
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        // Resume the game
        Time.timeScale = 1;
        // Load the main menu scene
        SceneManager.LoadScene("Main Menu");
    }
}