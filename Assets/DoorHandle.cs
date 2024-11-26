using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandle : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    private void Start()
    {

        grabInteractable = GetComponent<XRGrabInteractable>();
        UpdateHandleLockState(true);
    }

    public void UpdateHandleLockState(bool isLocked)
    {
        if (grabInteractable != null)
        {
            grabInteractable.enabled = !isLocked;
        }
    }
}