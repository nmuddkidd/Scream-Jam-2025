using UnityEngine;

public class SuperPaddleMixup : MonoBehaviour
{
    public static SuperPaddleMixup Instance;

    [SerializeField]
    GameObject[] SuperIndicators;

    [SerializeField]
    float duration = 10f;
    float starttime;

    void Awake()
    {
        Instance = this;
        starttime = float.MaxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup - starttime >= duration)
        {
            CleanUp();
        }
    }

    void CleanUp()
    {
        foreach (GameObject SuperIndicator in SuperIndicators)
            SuperIndicator.SetActive(false);
        starttime = float.MaxValue;
    }
    
    public void DoMixUp()
    {
        // Activate the mixup effect here
        Debug.Log("SuperPaddleMixup: DoMixup called - Activating Super Paddle!");
        foreach (GameObject SuperIndicator in SuperIndicators)        
            SuperIndicator.SetActive(true);        
        starttime = Time.realtimeSinceStartup;
    }
}
