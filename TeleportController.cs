using UnityEngine;
using Unity.XR.CoreUtils;

public class TeleportManager : MonoBehaviour
{
    [Header("References")]
    public XROrigin xrOrigin; // Assign your XR Origin / Camera Rig here

    public void TeleportTo(Transform destination)
    {
        if (xrOrigin == null)
        {
            Debug.LogError("TeleportManager: XR Origin not assigned!");
            return;
        }

        /* To teleport the player exactly to a spot, we must account 
           for the camera's offset from the origin center.
        */
        Vector3 cameraOffset = xrOrigin.Camera.transform.position - xrOrigin.transform.position;
        cameraOffset.y = 0; // Keep the floor height stable

        xrOrigin.transform.position = destination.position - cameraOffset;
        xrOrigin.transform.rotation = destination.rotation;
    }
}