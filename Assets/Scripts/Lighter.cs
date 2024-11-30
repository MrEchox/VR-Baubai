using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit; 

public class Lighter : MonoBehaviour
{
    public GameObject flame;                 
    public GameObject light;
    public AudioSource igniteSound;                  
    public InputActionProperty igniteAction;       
    public XRGrabInteractable grabInteractable;

    private bool isLit = false;                    
    private bool isGrabbed = true;
    private float extinguishTimer;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        flame.SetActive(false);
        light.SetActive(false);

        ScheduleNextExtinguish();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (isGrabbed && igniteAction.action.WasPressedThisFrame())
        {
            TryIgnite();
        }

        if (isLit)
        {
            extinguishTimer -= Time.deltaTime;
            if (extinguishTimer <= 0)
            {
                Extinguish();
                ScheduleNextExtinguish();
            }
        }
    }

    void TryIgnite()
    {
        if (igniteSound != null)
        {
            igniteSound.Play();
        }

        if (!isLit && Random.Range(0, 5) == 0) // 1 in 4 chance of lighting
        {
            isLit = true;
            flame.SetActive(isLit);
            light.SetActive(isLit);
        }
        else if (isLit)
        {
            ToggleLighter(false);
        }
    }


    void ToggleLighter(bool state)
    {
        isLit = state;
        flame.SetActive(isLit);
        light.SetActive(isLit);
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
            ToggleLighter(false); 
        }
    }

    public void Extinguish()
    {
        if (isLit)
        {
            ToggleLighter(false);
        }
    }
    void ScheduleNextExtinguish()
    {
        // Random interval between 30 and 60 seconds
        extinguishTimer = Random.Range(30f, 60f);
    }

}
