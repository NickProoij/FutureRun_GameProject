using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text leaderboardText;

    void Start()
    {
        DisplayLeaderboard();
    }

    public void PlayGame()
    {
        // Load the next scene (the game scene)
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }

    public void ResetLeaderboard()
    {
        // Reset the leaderboard and refresh the display
        Leaderboard.ResetLeaderboard();
        DisplayLeaderboard();
    }

    void DisplayLeaderboard()
    {
        // Display the leaderboard scores
        List<float> scores = Leaderboard.LoadScores();
        leaderboardText.text = "Leaderboard\n";
        for (int i = 0; i < scores.Count; i++)
        {
            leaderboardText.text += (i + 1).ToString() + ". " + Mathf.FloorToInt(scores[i]).ToString("D5") + "\n";
        }
    }
}