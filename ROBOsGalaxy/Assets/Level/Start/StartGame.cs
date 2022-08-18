using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private StartInput start;
    
    public void Awake()
    {
        start = new StartInput();
    }
    
    private void OnEnable()
    {
        start.Start.Game.Enable();
        start.Start.Game.performed += StartGameNow;
        
        
    }

    private void OnDestroy()
    {
        start.Start.Game.Disable();
        start.Start.Game.performed -= StartGameNow;
    }

    private void StartGameNow(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene("LavaLevel");
    }
}
