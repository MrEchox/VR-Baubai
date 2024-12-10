using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AlarmClockController : MonoBehaviour
{
    public AudioSource alarmSound;
    public AudioSource clickSound;
    public bool isAlarmOn = false;

    
    public Transform handle1, handle2;
    public Transform stick1, stick2;

    public float ringSpeed = 20f;
    public float maxAngle = 80f;

    public ExitDoorController exitDoorController;

    private Quaternion initialHandle1Rotation, initialHandle2Rotation;
    private Quaternion initialStick1Rotation, initialStick2Rotation;

    private void Start()
    {
        if (alarmSound != null) alarmSound.Stop();

        
        if (handle1 != null) initialHandle1Rotation = handle1.localRotation;
        if (handle2 != null) initialHandle2Rotation = handle2.localRotation;
        if (stick1 != null) initialStick1Rotation = stick1.localRotation;
        if (stick2 != null) initialStick2Rotation = stick2.localRotation;
    }

    private void Update()
    {
        if (isAlarmOn)
        {
            float angle = Mathf.Sin(Time.time * ringSpeed) * maxAngle;

            
            if (handle1 != null)
                handle1.localRotation = initialHandle1Rotation * Quaternion.Euler(angle, 0, 0);

            if (handle2 != null)
                handle2.localRotation = initialHandle2Rotation * Quaternion.Euler(-angle, 0, 0);

            if (stick1 != null)
                stick1.localRotation = initialStick1Rotation * Quaternion.Euler(angle, 0, 0);

            if (stick2 != null)
                stick2.localRotation = initialStick2Rotation * Quaternion.Euler(-angle, 0, 0);
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
            if (clickSound != null) clickSound.Play();
            isAlarmOn = false;

            // Reset objects to initial rotations
            if (handle1 != null) handle1.localRotation = initialHandle1Rotation;
            if (handle2 != null) handle2.localRotation = initialHandle2Rotation;
            if (stick1 != null) stick1.localRotation = initialStick1Rotation;
            if (stick2 != null) stick2.localRotation = initialStick2Rotation;
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
