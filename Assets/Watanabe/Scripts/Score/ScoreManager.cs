using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    public static float ResultScore { get; private set; } = 0f;
    public static Week ResultWeek { get; private set; } = Week.Monday;

    protected override bool DontDestroyOnLoad => true;

    public void ResetScore()
    {
        ResultScore = 0f;
        ResultWeek = Week.Monday;
    }

    public void SetScore(float score, Week week)
    {
        ResultScore = score;
        ResultWeek = week;
    }
}
