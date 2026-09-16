using UnityEngine;

public class CenturyInfoController : MonoBehaviour
{
    [Header("Century Screens")]
    [SerializeField] private GameObject[] centuryScreens;

    private void Start()
    {
        ShowCentury(0);
    }

    public void ShowCentury(int index)
    {
        if (index < 0 || index >= centuryScreens.Length)
            return;

        // Turn every screen off
        for (int i = 0; i < centuryScreens.Length; i++)
        {
            centuryScreens[i].SetActive(false);
        }

        // Turn selected screen on
        centuryScreens[index].SetActive(true);
    }
}