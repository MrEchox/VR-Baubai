using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class ReturnToOriginOnRelease : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public XRInteractionManager interactionManager;
    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;

    public Transform generator;
    public float maxUngrabDistance = 4.0f; // Maximum distance before ungrabbing
    public float smoothSpeed = 10.0f; // Return speed

    private Rigidbody handleRigidBody;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        handleRigidBody = GetComponent<Rigidbody>();

        if (grabInteractable != null)
        {
            // Store the initial local position and rotation relative to the generator
            originalLocalPosition = transform.localPosition;
            originalLocalRotation = transform.localRotation;

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

    private void Update()
    {
        if (grabInteractable.isSelected)
        {
            Vector3 direction = transform.position - generator.position;
            if (direction.magnitude > maxUngrabDistance)
            {
                var interactor = grabInteractable.interactorsSelecting[0]; // Get the first interactor
                interactionManager.SelectExit(interactor, grabInteractable);
            }
        }
    }

    private void OnObjectUngrabbed(SelectExitEventArgs args)
    {
        StartCoroutine(ReturnToLocalOrigin());
    }

    private IEnumerator ReturnToLocalOrigin()
    {
        handleRigidBody.isKinematic = true; // Temporarily disable physics

        float elapsedTime = 0f;
        float duration = 0.4f; // Duration of the return
        Vector3 startLocalPosition = transform.localPosition;
        Quaternion startLocalRotation = transform.localRotation;

        while (elapsedTime < duration)
        {
            // Interpolate position and rotation back to original local position and rotation
            transform.localPosition = Vector3.Lerp(startLocalPosition, originalLocalPosition, (elapsedTime / duration));
            transform.localRotation = Quaternion.Slerp(startLocalRotation, originalLocalRotation, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and rotation are set correctly
        transform.localPosition = originalLocalPosition;
        transform.localRotation = originalLocalRotation;

        handleRigidBody.isKinematic = false; // Re-enable physics
    }
}
