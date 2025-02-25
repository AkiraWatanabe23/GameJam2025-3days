using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    private float _resultScore = 0f;

    public float ResultScore => _resultScore;

    protected override bool DontDestroyOnLoad => true;

    public void SetScore(float score)
    {
        _resultScore = score;
    }
}
