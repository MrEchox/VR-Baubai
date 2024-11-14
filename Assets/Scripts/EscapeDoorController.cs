using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeDoorController : MonoBehaviour
{
    public Material closedMaterial;
    public Material openMaterial;
    public string sceneName = "4 WinScene";
    private Light areaLight;

    // Start is called before the first frame update
    void Start()
    {
        areaLight = gameObject.transform.GetChild(0).GetComponent<Light>();
        areaLight.enabled = false;
        GetComponent<Renderer>().material = closedMaterial;
    }

    void EnableExit()
    {
        areaLight.enabled = true;
        GetComponent<Renderer>().material = openMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
