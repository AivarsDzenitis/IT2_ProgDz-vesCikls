using UnityEngine;

public class ObjectiveDefinition : MonoBehaviour
{
    [Header("Objective")]
    public string objectiveID = "Coins";

    [Header("How many need to be found")]
    public int requiredAmount = 3;

    private void Start()
    {
        if (ObjectiveManager.Instance != null)
        {
            ObjectiveManager.Instance.RegisterObjective(
                objectiveID,
                requiredAmount
            );
        }
    }
}
