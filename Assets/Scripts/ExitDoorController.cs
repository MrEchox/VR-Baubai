using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorController : MonoBehaviour
{
    public Light areaLight;
    public AudioSource doorOpeningSound;
    public AudioSource ambientNoiseSound;
    public Material whiteGlowMaterial;
    public Transform door;
    public Vector3 openPosition;
    public Vector3 openRotation;

    public MeshRenderer doorRenderer; 

    private bool exitEnabled = false;

    private void Start()
    {
        if (areaLight != null)
        {
            areaLight.enabled = false;
            Debug.Log("Area light initially turned off.");
        }
    }

    public void EnableExit()
    {
        Debug.Log("Exit functionality enabled");

        exitEnabled = true;

        if (areaLight != null)
        {
            areaLight.enabled = true;
            Debug.Log("Area light enabled.");
        }

        if (doorOpeningSound != null)
        {
            doorOpeningSound.Play();
        }

        if (ambientNoiseSound != null)
        {
            ambientNoiseSound.Play();
        }

        if (doorRenderer != null && whiteGlowMaterial != null)
        {
            doorRenderer.material = whiteGlowMaterial;  
            Debug.Log("Door material changed to white glow.");
        }

        //door.localPosition = openPosition;
        //door.localRotation = Quaternion.Euler(openRotation);
        //Debug.Log("Door rotated.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (exitEnabled && other.CompareTag("Player"))
        {
            SceneManager.LoadScene("4 WinScene");
        }
    }
}
