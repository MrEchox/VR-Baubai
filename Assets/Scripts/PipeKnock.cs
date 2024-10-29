using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeKnock : MonoBehaviour
{
    public AudioSource knockSound;
    public float timeBetweenKnocks = 0.1f;
    public float timeBetweenPatterns = 2f;
    public float waitTimeBetweenRepeats = 3f;
    public int patternLength;
    public List<int> knockPattern;

    private void Start()
    {
        patternLength = Random.Range(3, 6); // 3-5 knock patterns

        knockPattern = GenerateKnockPattern(patternLength);
        StartCoroutine(PlayKnockPattern());
    }

    private IEnumerator PlayKnockPattern()
    {
        while (true) 
        {
            for (int i = 0; i < knockPattern.Count; i++)
            {
                yield return StartCoroutine(PlayKnocks(knockPattern[i])); // Play knocks
                yield return new WaitForSeconds(timeBetweenPatterns);
            }

            yield return new WaitForSeconds(waitTimeBetweenRepeats);
        }
    }

    private IEnumerator PlayKnocks(int knockCount)
    {
        for (int i = 0; i < knockCount; i++)
        {
            knockSound.Play(); // Play the knock sound
            yield return new WaitForSeconds(timeBetweenKnocks); // Wait between knocks
        }
    }

    private List<int> GenerateKnockPattern(int patternLength)
    {
        List<int> possibleKnocks = new List<int>();
        for (int i = 1; i <= 12; i++)
        {
            possibleKnocks.Add(i);
        }

        // Shuffle the list
        for (int i = 0; i < possibleKnocks.Count; i++)
        {
            int randomIndex = Random.Range(i, possibleKnocks.Count);
            int temp = possibleKnocks[i];
            possibleKnocks[i] = possibleKnocks[randomIndex];
            possibleKnocks[randomIndex] = temp;
        }

        List<int> pattern = new List<int>();
        for (int i = 0; i < patternLength; i++)
        {
            pattern.Add(possibleKnocks[i]);
        }

        return pattern;
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeKnock : MonoBehaviour
{
    public AudioSource knockSound;
    public float timeBetweenKnocks = 0.1f;
    public float timeBetweenPatterns = 2f;
    public float waitTimeBetweenRepeats = 3f;
    public int patternLength;
    public List<int> knockPattern;

    private IEnumerator DelayedWhileLoop()
    {
        
        while (true)
        {
            knockSound.Play();
            yield return new WaitForSeconds(1); // Delay for 1 second
        }
    }

    void Start()
    {
        StartCoroutine(DelayedWhileLoop());
    }
    
}*/