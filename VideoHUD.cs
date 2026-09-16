using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem;

public class VideoHUD : MonoBehaviour
{
    [Header("References")]
    public GameObject hud;
    public VideoPlayer videoPlayer;
    public Transform head;

    [Header("Input")]
    public InputActionReference toggleHUDAction;

    public float distanceFromHead = 1.5f;

    private void OnEnable()
    {
        toggleHUDAction.action.Enable();
        toggleHUDAction.action.performed += OnToggleHUD;
    }

    private void OnDisable()
    {
        toggleHUDAction.action.performed -= OnToggleHUD;
        toggleHUDAction.action.Disable();
    }

    private void Start()
    {
        if (hud != null)
            hud.SetActive(false);
    }

    private void OnToggleHUD(InputAction.CallbackContext ctx)
    {
        ToggleHUD();
    }

    void ToggleHUD()
    {
        bool show = !hud.activeSelf;

        if (show)
        {
            if (head != null)
            {
                hud.transform.position = head.position + head.forward * distanceFromHead;
                hud.transform.rotation = Quaternion.LookRotation(
                    hud.transform.position - head.position,
                    Vector3.up
                );
            }

            hud.SetActive(true);

            if (videoPlayer != null)
            {
                videoPlayer.Stop();
                videoPlayer.Play();
            }
        }
        else
        {
            if (videoPlayer != null)
                videoPlayer.Stop();

            hud.SetActive(false);
        }
    }
}