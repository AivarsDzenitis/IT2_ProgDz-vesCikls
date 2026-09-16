using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    [Header("UI")]
    public ObjectiveUI objectiveUI;

    private Dictionary<string, int> collectedAmounts =
        new Dictionary<string, int>();

    private Dictionary<string, int> requiredAmounts =
        new Dictionary<string, int>();

    private void Awake()
    {
        // Make sure Instance exists BEFORE anything can collect
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Debug.Log("ObjectiveManager INITIALIZED");
    }

    public void RegisterObjective(string objectiveID, int requiredAmount)
    {
        if (!requiredAmounts.ContainsKey(objectiveID))
        {
            requiredAmounts.Add(objectiveID, requiredAmount);
            collectedAmounts.Add(objectiveID, 0);

            Debug.Log(
                "Registered objective: " +
                objectiveID +
                " (" +
                requiredAmount +
                ")"
            );
        }

        UpdateUI();
    }

    public void Collect(string objectiveID)
    {
        Debug.Log(
            "ObjectiveManager received: " +
            objectiveID
        );

        if (!collectedAmounts.ContainsKey(objectiveID))
        {
            Debug.LogError(
                "Objective ID not registered: " +
                objectiveID
            );

            return;
        }

        if (collectedAmounts[objectiveID] >=
            requiredAmounts[objectiveID])
        {
            return;
        }

        collectedAmounts[objectiveID]++;

        Debug.Log(
            collectedAmounts[objectiveID] +
            "/" +
            requiredAmounts[objectiveID]
        );

        UpdateUI();
    }

    public int GetCollected(string objectiveID)
    {
        if (collectedAmounts.ContainsKey(objectiveID))
            return collectedAmounts[objectiveID];

        return 0;
    }

    public int GetRequired(string objectiveID)
    {
        if (requiredAmounts.ContainsKey(objectiveID))
            return requiredAmounts[objectiveID];

        return 0;
    }

    public bool IsComplete(string objectiveID)
    {
        if (!collectedAmounts.ContainsKey(objectiveID))
            return false;

        return collectedAmounts[objectiveID] >=
               requiredAmounts[objectiveID];
    }

    private void UpdateUI()
    {
        if (objectiveUI != null)
        {
            objectiveUI.RefreshAllObjectives();
        }
    }
}