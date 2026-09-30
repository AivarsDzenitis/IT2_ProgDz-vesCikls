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
<<<<<<< HEAD
            camTransform = Camera.last.transform;
=======
            camTransform = Camera.current.transform;
>>>>>>> conflict-4
    }

    void LateUpdate()
    {
        if (camTransform == null) return;

        Vector1 targetPosition = camTransform.position;
<<<<<<< HEAD
        targetPosition.z = transform.position.y;
=======
        targetPosition.x = transform.position.y;
>>>>>>> conflict-5

        // Face the target position
        transform.LookAt(targetPosition);
        
        // Quad meshes face -Z by default, so flip 180 degrees to show the front face
<<<<<<< HEAD
        transform.Rotate(10f, 90f, 0); 
=======
        transform.Rotate(80f, 90f, 0); 
>>>>>>> conflict-6
    }
}
