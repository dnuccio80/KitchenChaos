using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public enum Bindings
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        Interact_Alternate,
        Pause
    }

    private PlayerInputAction playerInputAction;

    private void Start()
    {
        Instance = this;
    }

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        playerInputAction.Player.Enable();

        playerInputAction.Player.Interaction.performed += Interaction_performed;
        playerInputAction.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputAction.Player.Pause.performed += Pause_performed;

        
    }

    private void OnDestroy()
    {
        playerInputAction.Player.Interaction.performed -= Interaction_performed;
        playerInputAction.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputAction.Player.Pause.performed -= Pause_performed;

        playerInputAction.Dispose();

    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interaction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        return inputVector;

    }

    public string GetBindingText(Bindings binding)
    {

        switch (binding)
        {
            default:
            case Bindings.Move_Up:
                return playerInputAction.Player.Move.bindings[1].ToDisplayString();
            case Bindings.Move_Down:
                return playerInputAction.Player.Move.bindings[2].ToDisplayString();
            case Bindings.Move_Left:
                return playerInputAction.Player.Move.bindings[3].ToDisplayString();
            case Bindings.Move_Right:
                return playerInputAction.Player.Move.bindings[4].ToDisplayString();
            case Bindings.Interact:
                return playerInputAction.Player.Interaction.bindings[0].ToDisplayString();
            case Bindings.Interact_Alternate:
                return playerInputAction.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Bindings.Pause:
                return playerInputAction.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    public void RebindBinding(Bindings bindings, Action OnActionRebound)
    {
        playerInputAction.Disable();

        InputAction inputAction;
        int buildIndex;

        switch (bindings)
        {
            default:
            case Bindings.Move_Up:
                inputAction = playerInputAction.Player.Move;
                buildIndex = 1;
                break;
            case Bindings.Move_Down:
                inputAction = playerInputAction.Player.Move;
                buildIndex = 2;
                break;
            case Bindings.Move_Left:
                inputAction = playerInputAction.Player.Move;
                buildIndex = 3;
                break;
            case Bindings.Move_Right:
                inputAction = playerInputAction.Player.Move;
                buildIndex = 4;
                break;
            case Bindings.Interact:
                inputAction = playerInputAction.Player.Interaction;
                buildIndex = 0;
                break;
            case Bindings.Interact_Alternate:
                inputAction = playerInputAction.Player.InteractAlternate;
                buildIndex = 0;
                break;
            case Bindings.Pause:
                inputAction = playerInputAction.Player.Pause;
                buildIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(buildIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                playerInputAction.Player.Enable();
                OnActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputAction.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
            .Start();
    }

   
}
