using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    [SerializeField]
    private Button _toTitleButton = default;
    [SerializeField]
    private Button _restartButton = default;

    [SerializeField]
    private Text _resultTimeText = default;
    [SerializeField]
    private Text _resultWeekText = default;

    [SerializeField]
    private Text _rankingFirstText = default;
    [SerializeField]
    private Text _rankingSecondText = default;
    [SerializeField]
    private Text _rankingThirdText = default;

    private readonly Dictionary<Week, string> _week = new()
    {
        { Week.Monday, "月曜日" },
        { Week.Tuesday, "火曜日" },
        { Week.Wednesday, "水曜日" },
        { Week.Thursday, "木曜日" },
        { Week.Friday, "金曜日" },
        { Week.Saturday, "土曜日" },
        { Week.Sunday, "日曜日" },
    };

    private void Start()
    {
        Initialize();
        AudioManager.Instance.StopBGM();
        Fade.Instance.StartFadeIn().OnComplete(() => AudioManager.Instance.PlaySE(SEType.ResultView));
    }

    private void Initialize()
    {
        _toTitleButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySE(SEType.Click);
            SceneLoader.FadeLoad(SceneName.Title);
        });
        _restartButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySE(SEType.Click);
            SceneLoader.FadeLoad(SceneName.InGame);
        });

        var resultTime = ScoreManager.ResultScore;

        var instance = new Ranking();
        var ranking = instance.Initialize(resultTime);

        _resultTimeText.text = resultTime.ToString("F2").Replace('.', ':');
        _resultWeekText.text = _week[ScoreManager.ResultWeek];

        _rankingFirstText.text = ranking[0].ToString("F2").Replace('.', ':');
        _rankingSecondText.text = ranking[1].ToString("F2").Replace('.', ':');
        _rankingThirdText.text = ranking[2].ToString("F2").Replace('.', ':');
    }
}
