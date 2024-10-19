using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit; 

public class Lighter : MonoBehaviour
{
    public GameObject flame;                         
    public AudioSource igniteSound;                  
    public InputActionProperty igniteAction;       
    public XRGrabInteractable grabInteractable;  

    private bool isLit = false;                    
    private bool isGrabbed = false;               

    // Start is called before the first frame update
    void Start()
    {
        flame.SetActive(false);

        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);
    }

    // Update is called once per frame
    void Update()
    {

        if (isGrabbed && igniteAction.action.WasPressedThisFrame())
        {
            ToggleLighter();
        }
    }

  
    void ToggleLighter()
    {
        isLit = !isLit;  

        flame.SetActive(isLit);

        if (igniteSound != null)
        {
            igniteSound.Play();
        }
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        isGrabbed = true;
    }

  
    private void OnRelease(XRBaseInteractor interactor)
    {
        isGrabbed = false;


        if (isLit)
        {
            ToggleLighter(); 
        }
    }

    private void OnDestroy()
    {
        grabInteractable.onSelectEntered.RemoveListener(OnGrab);
        grabInteractable.onSelectExited.RemoveListener(OnRelease);
    }
}
