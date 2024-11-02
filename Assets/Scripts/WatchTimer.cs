using UnityEngine;
using System.Collections;

public class DigitalWatch : MonoBehaviour
{
    public int timerMinutes = 21;
    private float minuteInterval = 60.0f;
    public AudioSource beepSound;
    public AudioSource alarmSound;

    public GameObject[] number1Bars;  // The 7 bars for the first digit
    public GameObject[] number2Bars;  // The 7 bars for the second digit
    public Material onMaterialWatch;  
    public Material offMaterialWatch; 

    public GameObject LED;
    public Material onMaterialLED;
    public Material offMaterialLED;

    private float flickerInterval = 0.95f;
    private float flickerDuration = 0.05f;
    private bool isLEDOn = true;


    // Define bar configurations for each digit (0 to 9)
    private readonly bool[][] digitPatterns = new bool[][]
    {
        // Left bottom, left top, top, right top, right bottom, bottom, middle
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

        InvokeRepeating("TurnOnLED", 0.0f, flickerInterval);
        InvokeRepeating("DecrementMinute", 0.0f, minuteInterval);
    }

    void Update()
    {
        // Get the digits
        int firstDigit = timerMinutes / 10;
        int secondDigit = timerMinutes % 10;

        // Update the bars based on the current time
        SetDigit(number1Bars, firstDigit);
        SetDigit(number2Bars, secondDigit);
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

    private void DecrementMinute()
    {
        timerMinutes--;

        if (timerMinutes < 0)
        {
            timerMinutes = 0;
            alarmSound.Play();
            CancelInvoke("DecrementMinute");
            return;
        }

        if (timerMinutes % 5 == 0)
            alarmSound.Play();
        else
        {
            beepSound.Play();
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
}
