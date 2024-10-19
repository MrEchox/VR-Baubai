using UnityEngine;

public class SaveBrickTransforms : MonoBehaviour
{
    public GameObject parentObject;  // The parent object whose children are the bricks

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))  // Press 'S' to save positions and rotations
        {
            foreach (Transform brick in parentObject.transform)
            {
                Debug.Log(brick.gameObject.name + " position: " + brick.position + " rotation: " + brick.rotation);
            }
        }
    }
}
