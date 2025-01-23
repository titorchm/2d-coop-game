using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // PlayerData
    [SerializeField] private PlayerData playerData;
    
    // InputSystem
    [SerializeField] private PlayerInput playerInput;
    
    // Physics
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Shift
    private bool _isShifting = false;
    
    // Dash
    private bool _isDashing = false;
    private bool _allowedToDash = true;

    private void OnEnable()
    {
        playerInput.OnMove += OnPlayerMoved;
        playerInput.OnJump += OnPlayerJump;
        playerInput.OnShift += OnPlayerShift;
        playerInput.OnShiftCanceled += OnShiftCanceled;
        playerInput.OnDash += OnPlayerDash;
    }

    void OnDisable()
    {
        playerInput.OnMove -= OnPlayerMoved;
        playerInput.OnJump -= OnPlayerJump;
        playerInput.OnShift += OnPlayerShift;
        playerInput.OnShiftCanceled -= OnShiftCanceled;
        playerInput.OnDash -= OnPlayerDash; 
    }

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (_isDashing)
        {
            return;
        }

        if (_isShifting)
        {
            Move(playerData.shiftSpeed);
        }
        else
        {
            Move(playerData.moveSpeed);
        }
        
    }

    private void Move(float moveSpeed)
    {
        float moveInput = playerInput.GetPlayerMovement().x;
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
    
    private void OnPlayerMoved(Vector2 inputDirection)
    {
        
        if (inputDirection.x == 1f)
        {
            transform.localScale = new Vector3(playerData.playerWidth, transform.localScale.y, transform.localScale.z);
        }
        else if (inputDirection.x == -1f)
        {
            transform.localScale = new Vector3(-playerData.playerWidth, transform.localScale.y, transform.localScale.z);
        }
    }
    
    private void OnPlayerJump()
    {
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerData.jumpForce);
        }
    }

    private void OnPlayerShift()
    {
        _isShifting = true;
    }
    
    private void OnShiftCanceled()
    {
        _isShifting = false;
    }
    
    private void OnPlayerDash()
    {
        if (_allowedToDash)
        {
            
            //StartCoroutine(Dash(playerInput.GetPlayerMovement().x));
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheck.position, -Vector3.up, 0.1f, groundLayer);
    }

    /*IEnumerator Dash(float dashDirection)
    {
        if (dashDirection == 0f)
        {
            dashDirection = transform.localScale.x;
        }
        
        float gravity = rb.gravityScale;
        
        _allowedToDash = false;
        _isDashing = true;
        rb.linearVelocity = new Vector2(dashDirection * playerData.dashForce, 0);
        rb.gravityScale = 0f;
        
        yield return new WaitForSeconds(playerData.dashTime);
        
        rb.gravityScale = gravity;
        _isDashing = false;

        // if (IsGrounded())
        // {
        //     _allowedToDash = true;
        // }
        
        yield return new WaitForSeconds(playerData.dashCooldown);
        
        _allowedToDash = true;
    }*/
}