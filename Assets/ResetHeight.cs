using UnityEngine;

public class ResetHeight : MonoBehaviour
{
    public float resetThresholdHeight = -10f;
    public float resetTargetHeight = 5f;

    public bool resetToFixedXZ = false;
    public Vector3 fixedPositionXZ = new Vector3(0, 5, 0);

    private void Update()
    {
        if (transform.position.y < resetThresholdHeight)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        Vector3 newPosition;

        if (resetToFixedXZ)
        {
            newPosition = new Vector3(fixedPositionXZ.x, resetTargetHeight, fixedPositionXZ.z);
        }
        else
        {
            newPosition = new Vector3(transform.position.x, resetTargetHeight, transform.position.z);
        }

        transform.position = newPosition;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Debug.Log($"{gameObject.name} was teleported back to {newPosition}");
    }
}
