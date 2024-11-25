using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmClockController : MonoBehaviour
{
    public Collider alarmClockCollider;
    public AudioSource alarmSound;
    public AudioSource clickSound;
    public bool isAlarmOn = false;

    void Start()
    {
        if (alarmSound != null)
        {
            alarmSound.Stop();
        }
    }

    public void StartAlarm()
    {
        if (alarmSound != null && !isAlarmOn)
        {
            Console.Write("Alarm started!");
            alarmSound.Play();
            isAlarmOn = true;
        }
    }

    private void StopAlarm()
    {
        if (alarmSound != null && isAlarmOn)
        {
            alarmSound.Stop();
            clickSound.Play();
            isAlarmOn = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAlarmOn && other.gameObject.CompareTag("Hand"))
        {
            StopAlarm();
            EnableExit();
        }
    }
    public void EnableExit()
    {
        Debug.Log("Exit enabled!");
    }

}
