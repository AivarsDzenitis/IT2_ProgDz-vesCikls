using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [System.Serializable]
    public class ObjectiveDisplay
    {
        public string objectiveID;

        public TMP_Text text;

        [Header("Optional")]
        public GameObject completedObject;
    }

    [Header("Objectives")]
    public List<ObjectiveDisplay> objectives = new List<ObjectiveDisplay>();

    public void RefreshAllObjectives()
    {
        if (ObjectiveManager.Instance == null)
            return;

        foreach (ObjectiveDisplay objective in objectives)
        {
            int collected =
                ObjectiveManager.Instance.GetCollected(objective.objectiveID);

            int required =
                ObjectiveManager.Instance.GetRequired(objective.objectiveID);

            bool complete =
                ObjectiveManager.Instance.IsComplete(objective.objectiveID);

            if (objective.text != null)
            {
                objective.text.text =
                    collected +
                    " / " +
                    required;

                if (complete)
                {
                    objective.text.text =
                        collected +
                        " / " +
                        required;
                }
            }

            if (objective.completedObject != null)
            {
                objective.completedObject.SetActive(complete);
            }
        }
    }

    private void Start()
    {
        RefreshAllObjectives();
    }
}
