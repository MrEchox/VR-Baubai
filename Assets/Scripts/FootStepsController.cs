using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsController : MonoBehaviour
{

    public AudioSource AudioSource;
    public float baseFootstepDelay = 0.7f;
    public float maxFootstepDelay = 3f;

    private CharacterController characterController;
    private bool isWalking;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        StartCoroutine(PlayFootSteps());
    }

    void Update()
    {
        isWalking = characterController != null && characterController.velocity.magnitude > 0.6f;
    }

    IEnumerator PlayFootSteps()
    {
        while (true)
        {
            if (isWalking)
            {
                float speed = characterController.velocity.magnitude;
                float speedRatio = Mathf.Clamp01(speed / 2f);

                float footstepDelay = Mathf.Lerp(maxFootstepDelay, baseFootstepDelay, speedRatio);

                AudioSource.Play();
                yield return new WaitForSeconds(footstepDelay);
            }
            else
            {
                yield return null;
            }
        }
    }
}
