using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;
using UnityEngine.Android;

public class CandleController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip successSound;
    public AudioClip extinguishSound;
    public GameObject pipes;

    public Material pentagramMaterial;
    public Light pentagramLight;

    public AudioSource fakeWallAudioSource;
    public GameObject fakeWall;
    private GameObject fakeWallModelBefore;
    private GameObject fakeWallModelAfter;

    public GameObject watchObject;
    private WatchTimer watch;

    public List<int> pattern;
    private int currentPatternIndex = 0;
    private List<CandleScript> allCandles = new List<CandleScript>();

    private bool isCompleted = false;

    void Start()
    {
        audioSource.clip = extinguishSound;

        fakeWallModelBefore = fakeWall.transform.Find("Fake-Wall_1").gameObject;
        if (fakeWallModelBefore == null)
            Debug.LogError("Fake-Wall_1 not found in the Fake Wall GameObject.");

        fakeWallModelAfter = fakeWall.transform.Find("Fake-Wall_2").gameObject;
        if (fakeWallModelAfter == null)
            Debug.LogError("Fake-Wall_2 not found in the Fake Wall GameObject.");

        watch = watchObject.GetComponent<WatchTimer>();

        pentagramMaterial.EnableKeyword("_EMISSION");
        pentagramLight.enabled = false;
        pentagramLight.intensity = 0;
        HDMaterial.SetEmissiveIntensity(pentagramMaterial, 0, UnityEditor.Rendering.HighDefinition.EmissiveIntensityUnit.Nits);

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
        if (isCompleted) return;

        if (currentPatternIndex < pattern.Count)
        {
            int expectedCandleNumber = pattern[currentPatternIndex];

            if (litCandle.CandleNum == expectedCandleNumber)
            {
                currentPatternIndex++;

                
                if (currentPatternIndex >= pattern.Count)
                {
                    Success();
                    currentPatternIndex = 0; 
                }
            }
            else
            {

                
                ResetCandles();


                if (watch != null)
                {
                    if (watch.timerMinutes > 4)  
                    {
                        watch.DecrementAndPlaySound(); 
                    }                
                }
                else
                    Debug.LogWarning("Watch is not assigned or found.");
            }
        }
    }

    private void ResetCandles()
    {
        if (isCompleted) return;

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
        if (isCompleted) return;

        isCompleted = true;

        if (audioSource != null && successSound != null)
        {
            fakeWallModelBefore.SetActive(false);
            fakeWallModelAfter.SetActive(true);

            fakeWallAudioSource.Play();
            StartCoroutine(ActivatePentagram());

            StartCoroutine(PlaySuccessSound(new WaitForSeconds(2.0f)));
        }
    }

    private IEnumerator ActivatePentagram()
    {
        float elapsed = 0f;
        pentagramLight.enabled = true;

        while (elapsed < 8.0f)
        {
            if (!this.enabled) yield break;

            elapsed += Time.deltaTime;

            float emission = Mathf.Lerp(0.0f, 10.0f, elapsed / 8.0f);
            float lightIntensity = Mathf.Lerp(0.0f, 60.0f, elapsed / 8.0f);

            pentagramLight.intensity = lightIntensity;
            HDMaterial.SetEmissiveIntensity(pentagramMaterial, emission, UnityEditor.Rendering.HighDefinition.EmissiveIntensityUnit.Nits);
            yield return null;
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
