using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class WatchTimer : MonoBehaviour
{
    public int timerMinutes = 10;
    private int timerSeconds = 60;
    private float minuteInterval = 60.0f;
    public AudioSource audioSource;
    public AudioClip alarmSound;
    public AudioClip beepSound;
    public AudioClip jumpscareSound;

    public GameObject[] number1Bars;  // The 7 bars for the first digit
    public GameObject[] number2Bars;  // The 7 bars for the second digit
    public Material onMaterialWatch;
    public Material offMaterialWatch;

    public GameObject LED;
    public Material onMaterialLED;
    public Material offMaterialLED;

    private float flickerInterval = 0.97f;
    private float flickerDuration = 0.03f;
    private bool isLEDOn = true;
    private bool isTimerRunning = false;
    private bool blinkAllBars = true;  // Control for blinking all bars


    // Bar configurations for each digit (0 to 9)
    private readonly bool[][] digitPatterns = new bool[][]
    {
        new bool[] { true, true, true, true, true, true, false},            // 0
        new bool[] { false, false, false, true, true, false, false},        // 1
        new bool[] { true, false, true, true, false, true, true},           // 2
        new bool[] { false, false, true, true, true, true, true},           // 3
        new bool[] { false, true, false, true, true, false, true},          // 4
        new bool[] { false, true, true, false, true, true, true},           // 5
        new bool[] { true, true, true, false, true, true, true},            // 6
        new bool[] { false, true, true, true, true, false, false},          // 7
        new bool[] { true, true, true, true, true, true, true},             // 8
        new bool[] { false, true, true, true, true, false, true}            // 9
    };

    private void Start()
    {    

        StartCoroutine(StartTimerWithDelay(5f)); // Start the timer after 5-second delay
        InvokeRepeating("TurnOnLED", 0.0f, flickerInterval);
        InvokeRepeating("BlinkAllBars", 0.5f, 1.0f); // Blink all bars every 1 second
    }

    private void Update()
    {
        // Blink all bars if the countdown hasn't started yet
        if (blinkAllBars)
        {
            SetAllBars(true);  // Turn on all bars for "88" look
        }
        else
        {
            // Display the actual time after countdown starts
            int firstDigit = (timerMinutes + 1) / 10;
            int secondDigit= (timerMinutes + 1) % 10;

            SetDigit(number1Bars, firstDigit);
            SetDigit(number2Bars, secondDigit);
        }
    }

    // Function to update the bars of a number based on the digit
    private void SetDigit(GameObject[] bars, int digit)
    {
        bool[] pattern = digitPatterns[digit];

        for (int i = 0; i < bars.Length; i++)
        {
            Renderer barRenderer = bars[i].GetComponent<Renderer>();
            barRenderer.material = pattern[i] ? onMaterialWatch : offMaterialWatch;
        }
    }

    // Coroutine to start the timer with a 5-second delay
    private IEnumerator StartTimerWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Play 3 beeps after 5 seconds
        for (int i = 0; i < 3; i++)
        {
            audioSource.clip = beepSound;
            audioSource.Play();
            yield return new WaitForSeconds(0.5f);
        }

        // Play a higher beep
        audioSource.clip = beepSound;
        audioSource.pitch = 1.5f;
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);

        // Start timer countdown and stop blinking
        audioSource.pitch = 1f; // Reset pitch
        isTimerRunning = true;
        blinkAllBars = false;  // Stop blinking

        InvokeRepeating("DecrementMinute", 0.0f, minuteInterval);
    }

    // Function to decrement the timer every minute
    private void DecrementMinute()
    {
        Debug.Log("DecrementMinute called!");

        if (!isTimerRunning) return;

        if (timerMinutes >= 1)
        {
            timerMinutes--;
            Debug.Log("Timer decremented: " + timerMinutes);
        }

        if (timerMinutes < 0)
        {
            isTimerRunning = false;
            timerMinutes = -1;
            audioSource.clip = alarmSound;
            audioSource.Play();
            CancelInvoke("DecrementMinute");

            // Play jumpscare sound after 3 seconds
            Invoke("PlayJumpscareSound", 3.0f);
            SceneManager.LoadScene("3 FailScene");
            return;
        }

        // Play alarm every 5 minutes, otherwise play beep
        if (timerMinutes % 5 == 0)
        {
            audioSource.clip = alarmSound;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = beepSound;
            audioSource.Play();
        }
    }

    // Function to decrement the timer manually
    public void DecrementAndPlaySound()
    {
        Debug.Log("DecrementAndPlaySound called!");

        if (!isTimerRunning) return;

        if (timerMinutes >= 1)
        {
            timerMinutes--;
            Debug.Log("Timer manually decremented: " + timerMinutes);

            // Play beep sound for every minute decrement
            audioSource.clip = beepSound;
            audioSource.Play();
        }

        if (timerMinutes < 0)
        {
            isTimerRunning = false;
            timerMinutes = -1;
            audioSource.clip = alarmSound;
            audioSource.Play();
            CancelInvoke("DecrementMinute");

            // Play jumpscare sound after 3 seconds
            Invoke("PlayJumpscareSound", 3.0f);
            SceneManager.LoadScene("3 FailScene");
        }
    }


    private void DecrementSecond()
    {
        if (!isTimerRunning) return;

        if (timerSeconds > 0)
        {
            timerSeconds--;
            audioSource.Play();
        }
        else
        {
            CancelInvoke("DecrementSecond");
            isTimerRunning = false;
            Invoke("PlayJumpscareSound", 0.3f);
            Invoke("LoadGameOverScene", 2f);
            return;
        }
    }

    private void BlinkAllBars()
    {
        if (blinkAllBars)
        {
            SetAllBars(!isLEDOn);
            isLEDOn = !isLEDOn;
        }
    }

    private void SetAllBars(bool state)
    {
        foreach (var bar in number1Bars)
        {
            bar.GetComponent<Renderer>().material = state ? onMaterialWatch : offMaterialWatch;
        }

        foreach (var bar in number2Bars)
        {
            bar.GetComponent<Renderer>().material = state ? onMaterialWatch : offMaterialWatch;
        }
    }

    private void TurnOnLED()
    {
        Renderer ledRenderer = LED.GetComponent<Renderer>();
        ledRenderer.material = onMaterialLED;
        Invoke("TurnOffLED", flickerDuration);
    }

    private void TurnOffLED()
    {
        Renderer ledRenderer = LED.GetComponent<Renderer>();
        ledRenderer.material = offMaterialLED;
    }

    private void PlayJumpscareSound()
    {
        audioSource.clip = jumpscareSound;
        audioSource.Play();
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene(2);
    }
}
