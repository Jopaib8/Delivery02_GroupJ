using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartInput : MonoBehaviour
{
    public InputActionAsset InputActions;
    public InputAction StartAction;

    private void OnEnable()
    {
        var uiActions = InputActions.FindActionMap("UI");
        StartAction = uiActions.FindAction("Start");
        if (StartAction != null)
        {
            StartAction.Enable();
            StartAction.performed += OnStartPerformed;
        }
    }

    private void OnDisable()
    {
        if (StartAction != null)
        {
            StartAction.performed -= OnStartPerformed;
            StartAction.Disable();
        }
    }

    private void OnStartPerformed(InputAction.CallbackContext context)
    {
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}