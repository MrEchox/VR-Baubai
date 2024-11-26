using UnityEngine;

public class DoorLock : MonoBehaviour
{
    public GameObject key;                  
    public DoorHandle doorHandle;          
    public Rigidbody lockRigidbody;         
    public Rigidbody frameRigidbody;      
    public AudioClip unlockSound;  
    private AudioSource audioSource;

    public bool isLocked = true;

    private void Start()
    {

        if (lockRigidbody != null)
            lockRigidbody.isKinematic = true;

        if (frameRigidbody != null)
            frameRigidbody.isKinematic = true;


        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }


        audioSource.playOnAwake = false;


        if (doorHandle != null)
        {
            doorHandle.UpdateHandleLockState(isLocked);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (isLocked && other.gameObject == key)
        {
            isLocked = false;
            Debug.Log("Door unlocked!");


            PlayUnlockSound();


            if (doorHandle != null)
            {
                doorHandle.UpdateHandleLockState(isLocked);
            }


            DropLockAndFrame();
        }
    }

    private void DropLockAndFrame()
    {

        if (lockRigidbody != null)
        {
            lockRigidbody.isKinematic = false;
            lockRigidbody.useGravity = true;
        }

        if (frameRigidbody != null)
        {
            frameRigidbody.isKinematic = false;
            frameRigidbody.useGravity = true;
        }
    }

    private void PlayUnlockSound()
    {
        if (unlockSound != null && audioSource != null)
        {
            audioSource.clip = unlockSound;
            audioSource.Play();
            Debug.Log("Playing unlock sound.");
        }
        
    }
}
