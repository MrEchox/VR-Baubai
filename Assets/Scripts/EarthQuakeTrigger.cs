using UnityEngine;

public class EarthquakeTrigger : MonoBehaviour
{
    public ScreenShake screenShake;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            screenShake.TriggerShake();
        }
    }
}
