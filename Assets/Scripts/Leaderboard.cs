using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private const int MaxEntries = 10;

    public static void SaveScore(float score)
    {
        List<float> scores = LoadScores();
        scores.Add(score);
        // Sort scores in descending order
        scores.Sort((a, b) => b.CompareTo(a));

        if (scores.Count > MaxEntries)
        {
            // Remove the lowest score if there are more than MaxEntries
            scores.RemoveAt(scores.Count - 1);
        }

        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetFloat("Leaderboard_" + i, scores[i]);
        }
        PlayerPrefs.Save();
    }

    public static List<float> LoadScores()
    {
        List<float> scores = new List<float>();

        for (int i = 0; i < MaxEntries; i++)
        {
            if (PlayerPrefs.HasKey("Leaderboard_" + i))
            {
                scores.Add(PlayerPrefs.GetFloat("Leaderboard_" + i));
            }
        }

        return scores;
    }

    public static void ResetLeaderboard()
    {
        for (int i = 0; i < MaxEntries; i++)
        {
            PlayerPrefs.DeleteKey("Leaderboard_" + i);
        }
        PlayerPrefs.Save();
    }
}