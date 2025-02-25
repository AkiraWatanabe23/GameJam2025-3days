using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ranking
{
    private List<float> _ranking = default;

    public List<float> Initialize(float latestScore)
    {
        //現状のランキング + 今回のスコアを取得
        _ranking = new()
        {
            PlayerPrefs.HasKey("1st") ? float.Parse(PlayerPrefs.GetString("1st")) : 0.0f,
            PlayerPrefs.HasKey("2nd") ? float.Parse(PlayerPrefs.GetString("2nd")) : 0.0f,
            PlayerPrefs.HasKey("3rd") ? float.Parse(PlayerPrefs.GetString("3rd")) : 0.0f,
            latestScore
        };

        var latestRanking = _ranking.OrderByDescending(data => data).ToList();
        //最新ランキングの更新
        PlayerPrefs.SetString("1st", latestRanking[0].ToString("F2"));
        PlayerPrefs.SetString("2nd", latestRanking[1].ToString("F2"));
        PlayerPrefs.SetString("3rd", latestRanking[2].ToString("F2"));

        return _ranking;
    }


    // Start is called before the first frame update
    void Start()
    {
        List<float> Scores = new List<float>()
        {
            PlayerPrefs.GetFloat("1", 0),
            PlayerPrefs.GetFloat("2", 0),
            PlayerPrefs.GetFloat("3", 0), GameManager.Instance.GetTime()
        };
        IEnumerable Ranking = Scores.OrderByDescending(x => x);
        int num = 0;
        foreach (float r in Ranking)
        {
            if(num < 3)
            {
                num++;
                PlayerPrefs.SetFloat(num.ToString(), r);
            }
        }
    }
}
