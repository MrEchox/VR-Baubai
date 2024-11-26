using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AlarmClockController : MonoBehaviour
{
    public AudioSource alarmSound;
    public AudioSource clickSound;
    public bool isAlarmOn = false;

    public ExitDoorController exitDoorController;

    private void Start()
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
            alarmSound.Play();
            isAlarmOn = true;
        }
    }

    private void StopAlarm()
    {
        if (alarmSound != null && isAlarmOn)
        {
            alarmSound.Stop();
            if (clickSound != null)
            {
                clickSound.Play();
            }
            isAlarmOn = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<XRDirectInteractor>() != null && isAlarmOn)
        {
            StopAlarm();
            if (exitDoorController != null)
            {
                exitDoorController.EnableExit();
            }
        }
    }
}
