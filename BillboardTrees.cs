using UnityEngine;

public class BillboardTree : MonoBehaviour
{
    private Transform camTransform;

    void Start()
    {
        if (Camera.main != null)
            camTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (camTransform == null) return;

        Vector3 targetPosition = camTransform.position;
        targetPosition.y = transform.position.y;

        // Face the target position
        transform.LookAt(targetPosition);
        
        // Quad meshes face -Z by default, so flip 180 degrees to show the front face
        transform.Rotate(90f, 90f, 0); 
    }
}