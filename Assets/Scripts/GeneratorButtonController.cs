using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GeneratorButtonController : MonoBehaviour
{
    public bool isOn = false;
    public Light pointLight;
    public AudioSource audioSource;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandTip"))
            if (!isOn)
                TurnOnButton();
    }

    private void TurnOnButton()
    {
        isOn = true;
        audioSource.Play();
        pointLight.enabled = true;
        transform.localRotation = Quaternion.Euler(-95, 0, 0);

    }
}
