using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    [Header("References")]
    public Transform target;

    [Header("Settings")]
    public Vector3 rotationOffset; 
    public float positionStrength = 1f;

    private Rigidbody rb;
    private Collider[] handColliders;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handColliders = GetComponentsInChildren<Collider>(); 
    }

    public void EnableHandCollider()
    {
        foreach(var item in handColliders)
        {
            if (item != null) item.enabled = true;
        }
    }

    public void EnableHandColliderDelay(float delay)
    {
        Invoke("EnableHandCollider", delay);
    }

    public void DisableHandCollider()
    {
        foreach(var item in handColliders)
        {
            if (item != null) item.enabled = false;
        }
    }

void FixedUpdate()
{
    if (target == null) return;

    // --- POSITION ---
    Vector3 positionDelta = target.position - transform.position;
    Vector3 velocity = (positionDelta / Time.fixedDeltaTime) * positionStrength;

    Vector3 desiredVelocity = positionDelta / Time.fixedDeltaTime * positionStrength;
    rb.linearVelocity = Vector3.ClampMagnitude(desiredVelocity, 15f); // 15 is a reasonable max speed

    // --- ROTATION ---
    transform.rotation = target.rotation * Quaternion.Euler(rotationOffset);
    rb.angularVelocity = Vector3.zero;
}
}