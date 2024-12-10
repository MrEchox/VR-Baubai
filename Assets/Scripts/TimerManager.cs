using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    private float timeSpentInScene;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes
        }
        else
        {
            Destroy(gameObject);  // Avoid duplicates
        }
    }

    void Update()
    {
        timeSpentInScene += Time.deltaTime;  // Count time spent in the current scene
    }

    public float GetTimeSpent()
    {
        return timeSpentInScene;
    }

    public void ResetTimer()
    {
        timeSpentInScene = 0f;  // Reset timer when needed
    }
}
