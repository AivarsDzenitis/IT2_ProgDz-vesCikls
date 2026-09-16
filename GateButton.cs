using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GateButton : MonoBehaviour
{
    [Header("Button Settings")]
    public float deadTime = 1.0f;
    private bool _deadTimeActive = false;
    public bool destroyOnPress = true;

    public UnityEvent onPressed;
    public UnityEvent onReleased;

    [Header("Animation Settings")]
    public Animator targetAnimator;
    public string animationStateName = "DropGate";

    [Header("Gate Collider Settings")]
    public Collider gateCollider; // <-- assign in Inspector
    public float colliderDelay = 0.5f; // delay to match animation timing

    private void OnTriggerEnter(Collider other)
    {
        if (_deadTimeActive) return;

        Debug.Log("BUTTON PRESSED");
        onPressed?.Invoke();

        PlayAnimation();

        // Start collider sync
        if (gateCollider != null)
            StartCoroutine(EnableColliderAfterDelay());

        if (destroyOnPress)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this == null || _deadTimeActive) return;

        onReleased?.Invoke();
        StartCoroutine(WaitForDeadTime());
    }

    IEnumerator WaitForDeadTime()
    {
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }

    void PlayAnimation()
    {
        if (targetAnimator == null) return;

        targetAnimator.Play(animationStateName, 0, 0f);
    }

    IEnumerator EnableColliderAfterDelay()
    {
        yield return new WaitForSeconds(colliderDelay);

        gateCollider.enabled = true; // block player
    }
}