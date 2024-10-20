using UnityEngine;

public class DigitalWatch : MonoBehaviour
{
    public GameObject[] number1Bars;  // The 7 bars for the first digit
    public GameObject[] number2Bars;  // The 7 bars for the second digit
    public Material onMaterial;       // Material to show when a bar is "on"
    public Material offMaterial;      // Material to show when a bar is "off"

    // Define bar configurations for each digit (0 to 9)
    private readonly bool[][] digitPatterns = new bool[][]
    {
        new bool[] { true, true, true, false, true, true, true },  // 0
        new bool[] { false, false, true, false, false, true, false },  // 1
        new bool[] { true, false, true, true, true, false, true },  // 2
        new bool[] { true, false, true, true, false, true, true },  // 3
        new bool[] { false, true, true, true, false, true, false },  // 4
        new bool[] { true, true, false, true, false, true, true },  // 5
        new bool[] { true, true, false, true, true, true, true },  // 6
        new bool[] { true, false, true, false, false, true, false },  // 7
        new bool[] { true, true, true, true, true, true, true },  // 8
        new bool[] { true, true, true, true, false, true, true }   // 9
    };

    void Update()
    {
        // Get current hour and minute
        int hour = System.DateTime.Now.Hour;
        int minute = System.DateTime.Now.Minute;

        // Get the digits
        int firstDigit = hour / 10;
        int secondDigit = hour % 10;

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
            barRenderer.material = pattern[i] ? onMaterial : offMaterial;
        }
    }
}
