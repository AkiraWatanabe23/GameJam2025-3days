﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 各シーンでのスコアの受け渡しを担うクラス </summary>
public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    private float _resultScore = 0f;

    public float ResultScore => _resultScore;

    protected override bool DontDestroyOnLoad => true;

    public void ResetScore() => _resultScore = 0f;

    public void SetScore(float score)
    {
        _resultScore = score;
    }
}
