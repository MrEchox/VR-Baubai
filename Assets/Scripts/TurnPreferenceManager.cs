using UnityEngine;

public class TurnPreferenceManager : MonoBehaviour
{
    public static TurnPreferenceManager Instance;

    public int turnPreference = 0;  // 0 for Snap Turn, 1 for Continuous Turn

    void Awake()
    {
        // Ensure only one instance of the manager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Load the turn preference from PlayerPrefs or set a default
            turnPreference = PlayerPrefs.GetInt("turn", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTurnPreference(int preference)
    {
        turnPreference = preference;
        PlayerPrefs.SetInt("turn", preference);
        PlayerPrefs.Save();
    }
}
