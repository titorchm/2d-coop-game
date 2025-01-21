using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // PlayerData
    [SerializeField] private PlayerData playerData;
    
    // InputSystem
    private InputSystem_Actions _inputSystem;
    
    // Physics
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Dash
    private bool _isDashing = false;
    private bool _allowedToDash = true;
    private float _dashTime = .2f;
    private float _dashCooldown = 1f;
    
    private void Awake()
    {
        _inputSystem = new InputSystem_Actions();
    }

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        _inputSystem.Player.Enable();
        _inputSystem.Player.Jump.started += OnPlayerJump;
        _inputSystem.Player.Dash.started += OnPlayerDash;
    }

    void Update()
    {
        if (_isDashing)
        {
            return;
        }
        
        Move();
    }

    private void Move()
    {
        float moveInput = _inputSystem.Player.Move.ReadValue<Vector2>().x;
        rb.linearVelocity = new Vector2(moveInput * playerData.moveSpeed, rb.linearVelocity.y);
    }
    
    private void OnPlayerJump(InputAction.CallbackContext obj)
    {
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerData.jumpForce);
        }
    }

    private void OnPlayerDash(InputAction.CallbackContext obj)
    {
        if (_allowedToDash)
        {
            StartCoroutine(Dash(_inputSystem.Player.Move.ReadValue<Vector2>()));
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheck.position, -Vector3.up, 0.1f, groundLayer);
    }

    IEnumerator Dash(Vector2 dashDirection)
    {
        float gravity = rb.gravityScale;
        
        _allowedToDash = false;
        _isDashing = true;
        rb.linearVelocity = new Vector2(dashDirection.x * playerData.dashForce, 0);
        rb.gravityScale = 0f;
        
        yield return new WaitForSeconds(_dashTime);
        
        rb.gravityScale = gravity;
        _isDashing = false;

        // if (IsGrounded())
        // {
        //     _allowedToDash = true;
        // }
        
        yield return new WaitForSeconds(_dashCooldown);
        
        _allowedToDash = true;
    }
}