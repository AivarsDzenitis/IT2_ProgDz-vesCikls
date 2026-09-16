using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{
    [Header("Button Settings")]
    public float deadTime = 1.0f;
    private bool _deadTimeActive = false;

    public UnityEvent onPressed;
    public UnityEvent onReleased;

    [Header("Teleport Settings")]
    public Transform playerRig;
    public Transform teleportTarget;
    public bool teleportOnTop = true;

    [Header("Height Offset")]
    [Tooltip("Extra height added after teleport")]
    public float heightOffset = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched by: " + other.name);

        if (_deadTimeActive) return;

        Debug.Log("BUTTON PRESSED");
        onPressed?.Invoke();
        TeleportPlayer();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_deadTimeActive) return;

        onReleased?.Invoke();
        StartCoroutine(WaitForDeadTime());
    }

    IEnumerator WaitForDeadTime()
    {
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }

    void TeleportPlayer()
    {
        if (playerRig == null || teleportTarget == null)
        {
            Debug.LogWarning("Teleport not set up!");
            return;
        }

        Vector3 targetPosition = teleportTarget.position;

        Transform camera = Camera.main.transform;
        Vector3 offset = playerRig.position - camera.position;

        if (teleportOnTop)
        {
            Collider col = teleportTarget.GetComponent<Collider>();
            if (col != null)
            {
                targetPosition.y = col.bounds.max.y;
            }
        }

        // Apply teleport with height offset
        playerRig.position = targetPosition + offset + new Vector3(0, heightOffset, 0);

        Debug.Log("Teleported!");
    }
}