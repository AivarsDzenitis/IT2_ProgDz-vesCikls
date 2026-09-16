using UnityEngine;

public class NPCWaypoint : MonoBehaviour
{
    [Tooltip("Animation played while the NPC is waiting here.")]
    public string animationName = "Walk";

    [Tooltip("Seconds to stay here.")]
    public float waitTime = 5f;
}