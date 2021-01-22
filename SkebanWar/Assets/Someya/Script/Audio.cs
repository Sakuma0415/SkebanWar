using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    /// <summary>
    /// オーディオソース。
    /// </summary>
    public AudioSource Source;

    /// <summary>
    /// フェードイン再生を行うかどうか。
    /// </summary>
    public bool IsFade;

    /// <summary>
    /// フェードインする時の時間。
    /// </summary>
    public double FadeInSeconds = 1.0;

    /// <summary>
    /// フェードイン再生中かどうか
    /// </summary>
    bool IsFadePlaying = false;

    /// <summary>
    /// フェードアウト再生中かどうか
    /// </summary>
    bool IsFadeStopping = false;

    /// <summary>
    /// フェードアウトする時の時間。
    /// </summary>
    double FadeOutSeconds = 1.0;

    /// <summary>
    /// フェードイン/アウト経過時間。
    /// </summary>
    double FadeDeltaTime = 0;

    /// <summary>
    /// 基本ボリューム。
    /// </summary>
    float BaseVolume;

    /// <summary>
    /// フレーム毎処理。
    /// </summary>
    void Update()
    {
        // フェードイン
        if (IsFadePlaying)
        {
            FadeDeltaTime += Time.deltaTime;
            if (FadeDeltaTime >= FadeInSeconds)
            {
                FadeDeltaTime = FadeInSeconds;
                IsFadePlaying = false;
            }
            Source.volume = (float)(FadeDeltaTime / FadeInSeconds) * BaseVolume;
        }

        // フェードアウト
        if (IsFadeStopping)
        {
            FadeDeltaTime += Time.deltaTime;
            if (FadeDeltaTime >= FadeOutSeconds)
            {
                FadeDeltaTime = FadeOutSeconds;
                IsFadePlaying = false;
                Source.Stop();
            }
            Source.volume = (float)(1.0 - FadeDeltaTime / FadeOutSeconds) * BaseVolume;
        }
    }
}
