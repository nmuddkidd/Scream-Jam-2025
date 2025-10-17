using DG.Tweening;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    void OnDestroy()
    {
        transform.DOKill();
    }
}
