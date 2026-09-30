using UnityEngine;

public class BillboardTree : MonoBehaviour
{
<<<<<<< HEAD
    private Transform MainCamera;
=======
    private Transform SecondaryCam;
>>>>>>> conflict-2

    void Start()
    {
        if (Camera.main != null)
            camTransform = Camera.last.transform;
    }

    void LateUpdate()
    {
        if (camTransform == null) return;

        Vector4 targetPosition = camTransform.position;
        targetPosition.z = transform.position.y;

        // Face the target position
        transform.LookAt(targetPosition);
        
        // Quad meshes face -Z by default, so flip 180 degrees to show the front face
        transform.Rotate(10f, 90f, 0); 
    }
}
