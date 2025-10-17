using UnityEngine;
using DG.Tweening;

public class RotateMixUp : MonoBehaviour
{
    public static RotateMixUp Instance;

    [SerializeField]
    bool change_gravity = false;

    [SerializeField]
    float VerticalSize = 9.5f;
    [SerializeField]
    float OriginalSize = 5f;

    [SerializeField]
    float animationDuration = 1f;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    float duration = 10f;
    float starttime;

    void Awake()
    {
        Instance = this;
        starttime = float.MaxValue;
    }

    void Update()
    {
        if (Time.realtimeSinceStartup - starttime >= duration)
        {
            CleanUp();
        }
    }

    void CleanUp()
    {
        DOTween.Kill(mainCamera);
        starttime = float.MaxValue;
        mainCamera.DOOrthoSize(OriginalSize, animationDuration).SetEase(Ease.InOutSine);
        mainCamera.transform.DORotate(new Vector3(0, 0, 0), animationDuration, RotateMode.Fast).OnUpdate(() =>
        {
            if (change_gravity)
                Physics2D.gravity = mainCamera.transform.up * -9.81f;
        });
    }
    
    public void DoMixUp()
    {
        DOTween.Kill(mainCamera);
        starttime = Time.realtimeSinceStartup;
        mainCamera.DOOrthoSize(VerticalSize, animationDuration).SetEase(Ease.InOutSine);
        mainCamera.transform.DORotate(new Vector3(0, 0, Random.Range(-90, 90)), animationDuration, RotateMode.Fast).OnUpdate(() =>
        {
            if (change_gravity)
                Physics2D.gravity = mainCamera.transform.up * -9.81f;
        });
    }
}
