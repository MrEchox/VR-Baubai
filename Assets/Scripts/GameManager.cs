using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

    // Variable to store the run time
    public float runTime;

    private void Awake()
    {
        // Ensure only one instance exists and persists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Increment the run time
        runTime += Time.deltaTime;
    }
}
