using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputAsset")]
public class InputManager : ScriptableObject, InputAsset.IGameplayActions
{
    private static InputAsset _inputAssets;

    private void OnEnable()
    {
        if (_inputAssets == null)
        {
            _inputAssets = new InputAsset();
            
            _inputAssets.Gameplay.SetCallbacks(this);
            
            SetGameplay();
        }
    }

    public static void SetGameplay()
    {
        _inputAssets.Gameplay.Enable();
    }
    
    public event Action<Vector2> MoveEvent;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
}