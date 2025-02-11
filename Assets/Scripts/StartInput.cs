using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartInput : MonoBehaviour
{
    public InputActionAsset inputActions;
    public InputAction startAction;

    private void OnEnable()
    {
        var uiActions = inputActions.FindActionMap("UI");
        startAction = uiActions.FindAction("Start");

        if (startAction != null)
        {
            startAction.Enable();
            startAction.performed += OnStartPerformed;
        }
    }

    private void OnDisable()
    {
        if (startAction != null)
        {
            startAction.performed -= OnStartPerformed;
            startAction.Disable();
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