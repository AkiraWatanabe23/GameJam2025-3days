using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    private Button _gameStartButton = default;

    private void Start()
    {
        _gameStartButton.onClick.AddListener(() => SceneLoader.FadeLoad(SceneName.InGame));

        Fade.Instance.StartFadeIn().OnComplete(() => AudioManager.Instance.PlayBGM(BGMType.Title));
    }
}
