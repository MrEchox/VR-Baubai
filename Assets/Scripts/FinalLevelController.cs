using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FinalLevelController : MonoBehaviour
{
    public int increasedSpeed = 3;
    public AudioSource heartbeatAudio;
    private AudioSource suspenseAudio;
    public ActionBasedContinuousMoveProvider playerMovement;
    public AlarmClockController alarmClockController;

    private bool hasStarted = false;

    private void Start()
    {
        suspenseAudio = GetComponentInChildren<AudioSource>();
        if (suspenseAudio == null)
            Debug.LogError("No suspenseAudio AudioSource found");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasStarted)
        {
            Console.Write("Accessed last level, playing sounds and modifying movement speed");
            if (heartbeatAudio != null) // Play heartbeat sound
            {
                heartbeatAudio.Play();
            }
            if (suspenseAudio != null) // Play suspense sound
            {
                suspenseAudio.Play();
            }
            if (playerMovement != null) // Modify player movement speed
            {
                playerMovement.moveSpeed = increasedSpeed;
            }
            if (alarmClockController != null) // Start alarm clock
            {
                alarmClockController.StartAlarm();
            }

            hasStarted = true;
        }
    }

}
