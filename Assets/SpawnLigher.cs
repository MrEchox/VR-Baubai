using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnLighter : MonoBehaviour
{
    public GameObject lighterPrefab; // Assign your lighter prefab in the inspector
    public Transform rightHandModel; // Reference to the right hand model

    void Start()
    {
        SpawnLighterInHand();
    }

    private void SpawnLighterInHand()
    {
        if (lighterPrefab != null && rightHandModel != null)
        {
            // Instantiate the lighter prefab
            GameObject lighter = Instantiate(lighterPrefab);

            // Set the lighter's parent to the right hand model's transform
            lighter.transform.SetParent(rightHandModel);

            // Adjust local position and rotation based on hand model
            // These values need to be adjusted based on your specific hand model
            lighter.transform.localPosition = new Vector3(0f, 0f, 0f); // Fine-tune these values
            lighter.transform.localRotation = Quaternion.Euler(0, -90, 0); // Adjust based on how the lighter should be oriented in the hand

            // Enable physics to prevent falling through the ground
            Rigidbody rb = lighter.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Ensure it's affected by physics
            }

            // Debug log for positioning
            Debug.Log("Lighter spawned in hand at local position: " + lighter.transform.localPosition);

            // If using XR Grab Interactable, add the component
            if (lighter.TryGetComponent(out XRGrabInteractable interactable))
            {
                interactable.interactionLayerMask = LayerMask.GetMask("Default"); // Ensure it interacts correctly
            }
        }
        else
        {
            Debug.LogError("Lighter prefab or right hand model is not assigned!");
        }
    }

    
}
