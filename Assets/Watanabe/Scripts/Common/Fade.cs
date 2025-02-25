using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary> フェードを管理するクラス </summary>
public class Fade : SingletonMonoBehaviour<Fade>
{
    [Tooltip("フェードさせるUI")]
    [SerializeField]
    private Image _fadePanel = default;
    [Tooltip("実行時間")]
    [SerializeField]
    private float _fadeTime = 1f;

    /// <summary> フェード対象(初期設定のものと別のものをフェードさせる場合に使う) </summary>
    private Image _fadeTarget = default;
    private Action[] _onComplete = default;

    protected override bool DontDestroyOnLoad => true;

    public bool IsFading { get; private set; }

    /// <summary> フェードイン開始 </summary>
    public Fade StartFadeIn(Image target = null)
    {
        _fadeTarget = target == null ? _fadePanel : target;

        StartCoroutine(FadeIn());
        return this;
    }

    /// <summary> フェードアウト開始 </summary>
    public Fade StartFadeOut(Image target = null)
    {
        _fadeTarget = target == null ? _fadePanel : target;

        StartCoroutine(FadeOut());
        return this;
    }

    /// <summary> フェード処理終了時に実行する処理がある場合、これを呼び出す </summary>
    public Fade OnComplete(params Action[] onComplete)
    {
        _onComplete = onComplete;
        return this;
    }

    private IEnumerator FadeIn()
    {
        IsFading = true;
        _fadeTarget.gameObject.SetActive(true);

        //α値（透明度）を 1 → 0 にする（少しずつ明るくする）
        float alpha = 1f;
        Color color = _fadeTarget.color;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / _fadeTime;

            if (alpha <= 0f) { alpha = 0f; }

            color.a = alpha;
            _fadeTarget.color = color;
            yield return null;
        }

        _fadeTarget.gameObject.SetActive(false);

        if (_onComplete != null)
        {
            foreach (var action in _onComplete) { action?.Invoke(); }
        }
        _onComplete = null;
        IsFading = false;
    }

    private IEnumerator FadeOut()
    {
        IsFading = true;
        _fadeTarget.gameObject.SetActive(true);

        //α値（透明度）を 0 → 1 にする（少しずつ暗くする）
        float alpha = 0f;
        Color color = _fadeTarget.color;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / _fadeTime;

            if (alpha >= 1f) { alpha = 1f; }

            color.a = alpha;
            _fadeTarget.color = color;
            yield return null;
        }

        if (_onComplete != null)
        {
            foreach (var action in _onComplete) { action?.Invoke(); }
        }
        _onComplete = null;
        IsFading = false;
    }
}