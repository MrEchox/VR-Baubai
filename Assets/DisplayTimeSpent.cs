using UnityEngine;
using TMPro;

public class DisplayTimeSpent : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Start()
    {
        if (TimerManager.Instance != null)
        {
            float timeSpent = TimerManager.Instance.GetTimeSpent();
            string minutes = Mathf.Floor(timeSpent / 60).ToString("00");
            string seconds = (timeSpent % 60).ToString("00");

            timeText.text = $"Congratulations! You survived and escaped the tunnels in {minutes}:{seconds}";
        }
        
        else
        {
            timeText.text = "Timer not found!";
        }
    }
}
