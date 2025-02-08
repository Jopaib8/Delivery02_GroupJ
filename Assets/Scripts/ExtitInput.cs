using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitInput : MonoBehaviour
{
    public InputActionAsset inputActions;
    public InputAction exitAction;

    private void OnEnable()
    {
        var uiActions = inputActions.FindActionMap("UI");
        exitAction = uiActions.FindAction("Exit");

        if (exitAction != null)
        {
            exitAction.Enable();
            exitAction.performed += OnExitPerformed;
        }
        else
        {
            Debug.LogError("No se encontró la acción 'Exit' en el Action Map 'UI'.");
        }
    }

    private void OnDisable()
    {
        if (exitAction != null)
        {
            exitAction.performed -= OnExitPerformed;
            exitAction.Disable();
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