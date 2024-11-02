using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CandleScript : MonoBehaviour
{
    public GameObject candleFlame;              // Reference to the candle's flame (e.g., particle system)
    public AudioSource igniteSound;             // Optional: Sound effect for when the candle is lit
    public Light candleLight;

    private CandleController candleController;


    public int CandleNum { get; private set; }
    private bool isLit = false;                 // Track if the candle is already lit
    public bool isCandleLit { get { return isLit; } }

    // Start is called before the first frame update
    void Start()
    {
        candleController = FindObjectOfType<CandleController>();

        string[] nameParts = gameObject.name.Split('-');

        if (nameParts.Length > 1 && int.TryParse(nameParts[1], out int candleNumber))
        {
            CandleNum = candleNumber; // Store the parsed number
        }

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
    private void LightCandle()
    {
        isLit = true;  // Set the candle as lit
        // Activate the candle's flame
        candleFlame.SetActive(true);
        candleLight.enabled = true;

        // Play the ignite sound (if assigned)
        if (igniteSound != null)
        {
            igniteSound.Play();
        }
        candleController.CheckCandle(this);
        // Optional: Add any additional effects like lighting adjustments, particles, etc.
    }

    public void ExtinguishCandle()
    {
        isLit = false;

        candleFlame.SetActive(false);
        candleLight.enabled = false;
    }
}
