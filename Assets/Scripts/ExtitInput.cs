using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitInput : MonoBehaviour
{
    public InputActionAsset InputActions;
    public InputAction ExitAction;

    private void OnEnable()
    {
        var uiActions = InputActions.FindActionMap("UI");
        ExitAction = uiActions.FindAction("Exit");
        if (ExitAction != null)
        {
            ExitAction.Enable();
            ExitAction.performed += OnExitPerformed;
        }
        else
        {
            Debug.LogError("UI NOT FOUND");
        }
    }

    private void OnDisable()
    {
        if (ExitAction != null)
        {            
            ExitAction.performed -= OnExitPerformed;
            ExitAction.Disable();
        }
    }

    private void OnExitPerformed(InputAction.CallbackContext context)
    {
        ExitGame();
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}