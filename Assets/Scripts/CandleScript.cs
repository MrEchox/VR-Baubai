using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : MonoBehaviour
{
    public GameObject candleFlame;   // Reference to the candle's flame (e.g., particle system)
    public AudioSource igniteSound;  // Optional: Sound effect for when the candle is lit

    private bool isLit = false;      // Track if the candle is already lit

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the candle flame is off initially
        candleFlame.SetActive(false);
    }

    // This method is called when something enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the flame of the lighter
        if (other.CompareTag("LighterFlame") && !isLit)  // Assuming you assign a tag to the lighter's flame
        {
            LightCandle();
        }
    }

    // Method to light the candle
    void LightCandle()
    {
        isLit = true;  // Set the candle as lit

        // Activate the candle's flame
        candleFlame.SetActive(true);

        // Play the ignite sound (if assigned)
        if (igniteSound != null)
        {
            igniteSound.Play();
        }

        // Optional: Add any additional effects like lighting adjustments, particles, etc.
    }
}
