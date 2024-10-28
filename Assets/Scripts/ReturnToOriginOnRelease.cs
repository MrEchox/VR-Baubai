using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReturnToOriginOnRelease : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;

            grabInteractable.selectExited.AddListener(OnObjectUngrabbed);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnObjectUngrabbed);
        }
    }

    private void OnObjectUngrabbed(SelectExitEventArgs args)
    {
        // Return the object to its original position and rotation
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
