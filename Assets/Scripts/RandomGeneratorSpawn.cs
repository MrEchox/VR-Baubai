using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneratorSpawn : MonoBehaviour
{
    public Vector3[] presetPositions;
    public Quaternion[] presetRotations;

    void Start()
    {
        if (presetPositions.Length > 0 && presetRotations.Length > 0)
        {
            if (presetPositions.Length == presetRotations.Length)
            {
                int randomIndex = Random.Range(0, presetPositions.Length);

                transform.localPosition = presetPositions[randomIndex];
                transform.rotation = presetRotations[randomIndex];
            }
            else
                Debug.LogWarning("Preset positions and rotations arrays must be of the same length!");
        }
        else
            Debug.LogWarning("No preset positions or rotations defined!");
    }
}
