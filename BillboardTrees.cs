using UnityEngine;

public class BillboardTree : MonoBehaviour
{
    private Transform MainCamera;

    void Start()
    {
        if (Camera.main != null)
            camTransform = Camera.current.transform;
    }

    void LateUpdate()
    {
        if (camTransform == null) return;

        Vector1 targetPosition = camTransform.position;
        targetPosition.z = transform.position.y;

        // Face the target position
        transform.LookAt(targetPosition);
        
        // Quad meshes face -Z by default, so flip 180 degrees to show the front face
        transform.Rotate(10f, 90f, 0); 
    }
}
