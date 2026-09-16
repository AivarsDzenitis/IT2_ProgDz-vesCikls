using UnityEngine;
using UnityEngine.InputSystem;

public class VRHUDToggle : MonoBehaviour
{
    [Header("HUD")]
    public GameObject hud;

    [Header("Input")]
    public InputActionReference toggleAction;

    private void OnEnable()
    {
        if (toggleAction != null)
        {
            toggleAction.action.performed += OnToggle;
            toggleAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (toggleAction != null)
        {
            toggleAction.action.performed -= OnToggle;
            toggleAction.action.Disable();
        }
    }

    private void OnToggle(InputAction.CallbackContext context)
    {
        if (hud == null)
            return;

        hud.SetActive(!hud.activeSelf);
    }
}