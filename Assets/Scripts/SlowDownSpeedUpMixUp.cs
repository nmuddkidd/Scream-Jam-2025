using System.Collections;
using UnityEngine;

public class SlowDownSpeedUpMixUp : MonoBehaviour
{
    public static SlowDownSpeedUpMixUp Instance;

    [SerializeField]
    AnimationCurve curve;

    [SerializeField]
    float minDuration = 5f, maxDuration = 10f;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    public void DoMixUp()
    {
        StopAllCoroutines();
        float originalTimeScale = Time.timeScale;
        float duration = Random.Range(minDuration, maxDuration);
        float elapsed = 0f;

        StartCoroutine(ChangeTimeScaleOverTime(originalTimeScale, duration, elapsed));
    }

    private IEnumerator ChangeTimeScaleOverTime(float originalTimeScale, float duration, float elapsed)
    {
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            Time.timeScale = curve.Evaluate(t);
            yield return null;
        }

        Time.timeScale = originalTimeScale;
    }
}
