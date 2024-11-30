using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
    [Range(0, 1)]
    public float intensity;
    public float duration;

    public void TriggerHaptic(XRBaseController controller)
    {
        if (controller != null && intensity > 0)
        {
            controller.SendHapticImpulse(intensity, duration);
        }
    }
}

public class ScreenShake : MonoBehaviour
{
    public Transform cameraOffset;
    public float duration = 0.5f;
    public float magnitude = 0.1f;
    private Vector3 originalLocalPosition;

    public XRBaseController leftController;
    public XRBaseController rightController;
    public Haptic hapticFeedback;

    private void Start()
    {
        if (cameraOffset == null)
        {
            
        }
    }

    public void TriggerShake()
    {
        if (cameraOffset != null)
        {
            originalLocalPosition = cameraOffset.localPosition;
            StartCoroutine(Shake());
            TriggerHaptics();
        }
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

           
            cameraOffset.localPosition = originalLocalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraOffset.localPosition = originalLocalPosition;
    }

    private void TriggerHaptics()
    {
        if (hapticFeedback != null)
        {
            hapticFeedback.TriggerHaptic(leftController);
            hapticFeedback.TriggerHaptic(rightController);
        }
    }
}
