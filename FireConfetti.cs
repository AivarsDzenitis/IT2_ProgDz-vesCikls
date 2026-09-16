using UnityEngine;

public class FireConfetti : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Fire()
    {
        Debug.Log("🔥 FireConfetti.Fire() CALLED");

        if (ps == null)
        {
            Debug.LogError("No ParticleSystem found on this object!");
            return;
        }

        ps.Play();
    }
}