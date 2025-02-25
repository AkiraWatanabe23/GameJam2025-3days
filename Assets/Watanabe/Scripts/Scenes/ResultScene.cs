using UnityEngine;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    [SerializeField]
    private Button _toTitleButton = default;
    [SerializeField]
    private Button _restartButton = default;

    private void Start()
    {
        Initialize();
        Fade.Instance.StartFadeIn().OnComplete(() => AudioManager.Instance.PlayBGM(BGMType.Result));
    }

    private void Initialize()
    {
        _toTitleButton.onClick.AddListener(() => SceneLoader.FadeLoad(SceneName.Title));
        _restartButton.onClick.AddListener(() => SceneLoader.FadeLoad(SceneName.InGame));

        //todo : ランキング更新処理
    }
}
