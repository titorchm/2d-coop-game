using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "ScriptableObjects/PlayerInput")]
public class PlayerInput : ScriptableObject
{
    private InputSystem_Actions _inputSystemActions;
    
    public event Action<Vector2> OnMove;
    public event Action OnJump;
    public event Action OnShift;
    public event Action OnShiftCanceled;
    public event Action OnDash;
    public event Action OnInteract;
    
    private void OnEnable()
    {
        if (_inputSystemActions == null)
        {
            _inputSystemActions = new InputSystem_Actions();
        }
        _inputSystemActions.Enable();
        _inputSystemActions.Player.Move.performed += OnPlayerMove;
        _inputSystemActions.Player.Jump.performed += OnPlayerJump;
        _inputSystemActions.Player.Shift.performed += OnPlayerShift;
        _inputSystemActions.Player.Shift.canceled += OnPlayerShiftCanceled;
        _inputSystemActions.Player.Dash.performed += OnPlayerDash;
        _inputSystemActions.Player.Interact.performed += OnPlayerInteract;
    }

    private void OnDisable()
    {
        _inputSystemActions.Disable();
        _inputSystemActions.Player.Move.performed -= OnPlayerMove;
        _inputSystemActions.Player.Jump.performed -= OnPlayerJump;
        _inputSystemActions.Player.Shift.performed -= OnPlayerShift;
        _inputSystemActions.Player.Shift.canceled -= OnPlayerShiftCanceled;
        _inputSystemActions.Player.Dash.performed -= OnPlayerDash;
        _inputSystemActions.Player.Interact.performed -= OnPlayerInteract;
    }

    public void DisableMovement()
    {
        _inputSystemActions.Disable();
    }
    
    public Vector2 GetPlayerMovement()
    {
        return _inputSystemActions.Player.Move.ReadValue<Vector2>();
    }

    private void OnPlayerMove(InputAction.CallbackContext ctx)
    {
        OnMove?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void OnPlayerJump(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke();
    }
    
    private void OnPlayerShift(InputAction.CallbackContext obj)
    {
        OnShift?.Invoke();
    }
    private void OnPlayerShiftCanceled(InputAction.CallbackContext obj)
    {
        OnShiftCanceled?.Invoke();
    }
    
    private void OnPlayerDash(InputAction.CallbackContext ctx)
    {
        OnDash?.Invoke();
    }

    private void OnPlayerInteract(InputAction.CallbackContext ctx)
    {
        OnInteract?.Invoke();
    }
}
