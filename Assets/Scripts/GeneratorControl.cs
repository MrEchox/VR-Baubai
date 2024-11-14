using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using Unity.XR.CoreUtils;

public class GeneratorControl : MonoBehaviour
{
    public Transform handle; 
    public AudioSource pullAudioSource;
    public AudioSource generatorAudioSource;
    public GeneratorButtonController button;
    public GameObject controlledDoorHandle;
    public Light doorLight;
    public AudioSource audioSource;

    private XRGrabInteractable grabInteractable;
    private XRGrabInteractable doorGrabScript;
    private Vector3 previousPosition;

    public bool isGeneratorOn { get  { return isOn; } }

    public float pullSpeedThreshold = 5f; // Minimum speed to activate
    public float pullDistanceThreshold = 0.1f; // Minimum distance to activate
    public float activationDelay = 0.5f; // Delay before allowing activation after grabbing
    private bool isActivationAllowed = false; // Flag to control activation state
    private bool isOn;

    private void Start()
    {
        grabInteractable = handle.GetComponent<XRGrabInteractable>();
        UpdateHandlePoistion(handle.position);
        isOn = false;

        doorGrabScript = controlledDoorHandle.GetComponent<XRGrabInteractable>();
        doorGrabScript.enabled = false;

        doorLight.enabled = false;
    }

    private void Update()
    {
        if (grabInteractable.isSelected) // Is handle grabbed
        {
            if (!isActivationAllowed)
                StartCoroutine(AllowActivationAfterDelay());

            float pullSpeed = Vector3.Distance(previousPosition, handle.position) / Time.deltaTime;
            float distanceMoved = Vector3.Distance(previousPosition, handle.position);
            Vector3 direction = handle.position - previousPosition;

            bool isPulledAway = Vector3.Dot((handle.position - transform.position).normalized, direction.normalized) > 0;

            if (distanceMoved > pullDistanceThreshold && isPulledAway && isActivationAllowed)
            {
                if (pullSpeed > pullSpeedThreshold)
                {
                    pullAudioSource.Play(); 

                    if (button.isOn && Random.value < 0.1f) // 1/10 chance and button on
                    {
                        TurnOnGenerator();
                    }
                }
            }

            previousPosition = handle.position;
        }
        else
        {
            previousPosition = handle.position; // To avoid pulling speed calculation errors
            isActivationAllowed = false;
        }
    }

    private IEnumerator AllowActivationAfterDelay()
    {
        yield return new WaitForSeconds(activationDelay); 
        isActivationAllowed = true; 
    }

    private void TurnOnGenerator()
    {
        if (!generatorAudioSource.isPlaying)
        {
            isOn = true;
            generatorAudioSource.Play();

            StartCoroutine(ActivateDoorComponentsAfterDelay());
        }
    }

    private IEnumerator ActivateDoorComponentsAfterDelay()
    {
        yield return new WaitForSeconds(5f);

        doorGrabScript.enabled = true;
        doorLight.enabled = true;
        audioSource.Play();
    }

    private void UpdateHandlePoistion(Vector3 newPos)
    {
        handle.position = newPos;
        previousPosition = newPos;
    }
}
