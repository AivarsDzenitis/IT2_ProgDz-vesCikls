using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;

    void Update()
    {
        float distance = Vector3.Distance(objectA.position, objectB.position);

        Debug.Log("Distance: " + distance);
    }
}