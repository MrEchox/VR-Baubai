using UnityEngine;

public class WireRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 startPosition;

    void Start()
    {
        // Initialize
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;  
        startPosition = transform.position;

        // Wire appearance
        lineRenderer.startWidth = 0.005f;
        lineRenderer.endWidth = 0.005f;
        Material lineMaterial = new Material(Shader.Find("Unlit/Color"));
        lineMaterial.color = Color.black;
        lineRenderer.material = lineMaterial;
    }

    void Update()
    {
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, startPosition);      // Fixed start position
            lineRenderer.SetPosition(1, transform.position); // Dynamic end point at handle's current position
        }
        else
        {
            Debug.LogError("LineRenderer is not initialized.");
        }
    }
}
