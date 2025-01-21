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

    // Dash
    private bool _isDashing = false;
    private bool _allowedToDash = true;
    private float _dashTime = .2f;
    private float _dashCooldown = 1f;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        playerInput.OnMove += OnPlayerMoved;
        playerInput.OnJump += OnPlayerJump;
        playerInput.OnDash += OnPlayerDash;
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
        float moveInput = playerInput.GetPlayerMovement().x;
        rb.linearVelocity = new Vector2(moveInput * playerData.moveSpeed, rb.linearVelocity.y);
    }
    
    private void OnPlayerMoved(Vector2 inputDirection)
    {
        
        if (inputDirection.x == 1f)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
        else if (inputDirection.x == -1f)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
    }
    
    private void OnPlayerJump()
    {
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerData.jumpForce);
        }
    }

    private void OnPlayerDash()
    {
        if (_allowedToDash)
        {
            StartCoroutine(Dash(playerInput.GetPlayerMovement().x));
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheck.position, -Vector3.up, 0.1f, groundLayer);
    }

    IEnumerator Dash(float dashDirection)
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