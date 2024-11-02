using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CandleController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip successSound;
    public AudioClip extinguishSound;
    public GameObject pipes;

    public AudioSource fakeWallAudioSource;
    public GameObject fakeWall;
    private GameObject fakeWallModelBefore;
    private GameObject fakeWallModelAfter;

    public List<int> pattern;
    private int currentPatternIndex = 0;
    private List<CandleScript> allCandles = new List<CandleScript>();

    void Start()
    {
        audioSource.clip = extinguishSound;
        fakeWallModelBefore = fakeWall.transform.Find("Fake-Wall_1").gameObject;
        fakeWallModelAfter = fakeWall.transform.Find("Fake-Wall_2").gameObject;

        fakeWallModelBefore.SetActive(true);
        fakeWallModelAfter.SetActive(false);

        PipeKnock pipeKnock = pipes.GetComponent<PipeKnock>();
        if (pipeKnock != null)
        {
            pattern = pipeKnock.pattern;
            Debug.Log("Pattern retrieved: " + string.Join(", ", pattern)); // Debugging output
        }
        else
        {
            Debug.LogError("PipeKnock component not found on pipes GameObject.");
        }

        // Get each candle
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Candle-"))
            {
                CandleScript candleScript = child.GetComponent<CandleScript>();
                if (candleScript != null)
                {
                    allCandles.Add(candleScript);
                }
            }
        }

    }
    public void CheckCandle(CandleScript litCandle)
    {
        if (currentPatternIndex < pattern.Count)
        {
            int expectedCandleNumber = pattern[currentPatternIndex];

            // Check if the lit candle matches the expected candle number
            if (litCandle.CandleNum == expectedCandleNumber)
            {
                currentPatternIndex++;

                // Check if the pattern is completed
                if (currentPatternIndex >= pattern.Count)
                {
                    Success();
                    currentPatternIndex = 0; // Reset for the next pattern
                }
            }
            else
            {
                // Reset candles if the wrong candle is lit
                ResetCandles();
            }
        }
    }

    private void ResetCandles()
    {
        foreach (CandleScript candle in allCandles)
        {
            candle.ExtinguishCandle();

        }

        if (audioSource != null && extinguishSound != null)
        {
            audioSource.PlayOneShot(extinguishSound);
        }

        currentPatternIndex = 0;
    }
    private void Success()
    {
        if (audioSource != null && successSound != null)
        {
            fakeWallModelBefore.SetActive(false);
            fakeWallModelAfter.SetActive(true);

            fakeWallAudioSource.Play();

            StartCoroutine(PlaySuccessSound(new WaitForSeconds(2.0f)));
        }
    }

    private IEnumerator PlaySuccessSound(WaitForSeconds wait)
    {
        yield return wait;
        audioSource.clip = successSound;
        audioSource.Play();

        this.enabled = false; // Turn it off, so player can light the rest of the candles ;)
    }

}
