using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine;

public class HapticFeedback : MonoBehaviour
{
    public XRBaseController controller;
    public float amplitude = 0.5f;
    public float duration = 0.2f;

    public void TriggerHaptic()
    {
        if (controller != null)
        {
            controller.SendHapticImpulse(amplitude, duration);
        }
    }
}
