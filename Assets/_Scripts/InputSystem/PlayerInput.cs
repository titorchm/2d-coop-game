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
    public event Action OnCollect;
    public event Action OnCollectStarted;
    public event Action OnCollectCanceled;
    public event Action OnInteract;
    public event Action OnInventoryToggled;
    
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
        _inputSystemActions.Player.Interact.performed += OnPlayerInteract;
        _inputSystemActions.Player.Collect.performed += OnPlayerCollect;
        _inputSystemActions.Player.Collect.started += OnPlayerCollectStarted;
        _inputSystemActions.Player.Collect.canceled += OnPlayerCollectCanceled;
        _inputSystemActions.Player.InventoryToggle.performed += OnInventoryToggle;
    }

    private void OnDisable()
    {
        _inputSystemActions.Disable();
        _inputSystemActions.Player.Move.performed -= OnPlayerMove;
        _inputSystemActions.Player.Jump.performed -= OnPlayerJump;
        _inputSystemActions.Player.Shift.performed -= OnPlayerShift;
        _inputSystemActions.Player.Shift.canceled -= OnPlayerShiftCanceled;
        _inputSystemActions.Player.Interact.performed -= OnPlayerInteract;
        _inputSystemActions.Player.Collect.performed -= OnPlayerCollect;
        _inputSystemActions.Player.Collect.started -= OnPlayerCollectStarted;
        _inputSystemActions.Player.Collect.canceled -= OnPlayerCollectCanceled;
        _inputSystemActions.Player.InventoryToggle.performed -= OnInventoryToggle;
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
    
    private void OnPlayerShift(InputAction.CallbackContext ctx)
    {
        OnShift?.Invoke();
    }
    private void OnPlayerShiftCanceled(InputAction.CallbackContext ctx)
    {
        OnShiftCanceled?.Invoke();
    }

    private void OnPlayerInteract(InputAction.CallbackContext ctx)
    {
        OnInteract?.Invoke();
    }
    
    private void OnPlayerCollect(InputAction.CallbackContext ctx)
    {
        OnCollect?.Invoke();
    }

    private void OnPlayerCollectStarted(InputAction.CallbackContext ctx)
    {
        OnCollectStarted?.Invoke();
    }
    
    private void OnPlayerCollectCanceled(InputAction.CallbackContext ctx)
    {
        OnCollectCanceled?.Invoke();
    }
    
    private void OnInventoryToggle(InputAction.CallbackContext ctx)
    {
        OnInventoryToggled?.Invoke();
    }
}
