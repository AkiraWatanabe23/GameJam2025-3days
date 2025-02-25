using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    private Button _gameStartButton = default;

    [SerializeField]
    private Button[] _playSEButtons = default;

    private void Start()
    {
        _gameStartButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySE(SEType.FadeToGame);
            SceneLoader.FadeLoad(SceneName.InGame);
        });

        foreach (var button in _playSEButtons) { button.onClick.AddListener(() => AudioManager.Instance.PlaySE(SEType.Click)); }

        Fade.Instance.StartFadeIn().OnComplete(() => AudioManager.Instance.PlayBGM(BGMType.Title));
    }
}
