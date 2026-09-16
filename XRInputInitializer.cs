using UnityEngine;
using UnityEngine.InputSystem;

public class XRInputInitializer : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    private void Awake()
    {
        if (inputActions != null)
        {
            inputActions.Enable();
            Debug.Log("XR Input Actions successfully enabled!");
        }
        else
        {
            Debug.LogError("XR Input Actions asset is missing from the Initializer script slot!");
        }
    }
}