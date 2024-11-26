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

    private Renderer doorRenderer;

    private void Start()
    {
       
        doorRenderer = GetComponent<Renderer>();

      
        if (areaLight != null)
        {
            areaLight.enabled = false;
            Debug.Log("Area light initially turned off.");
        }

      
        if (door != null)
        {
            door.localPosition = Vector3.zero;
            door.localRotation = Quaternion.identity;
        }
    }

    
    public void EnableExit()
    {
        Debug.Log("Exit functionality enabled");

      
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
        }

        if (door != null)
        {
            door.localPosition = openPosition;
            door.localRotation = Quaternion.Euler(openRotation);
        }
    }

 
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("4 WinScene");
        }
    }
}
