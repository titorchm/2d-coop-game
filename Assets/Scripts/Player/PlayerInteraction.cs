using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    void OnEnable()
    {
        playerInput.OnInteract += OnPlayerInteract;
    }

    private void OnDisable()
    {
        playerInput.OnInteract -= OnPlayerInteract;
    }

    private void OnPlayerInteract()
    {
        Debug.Log("Interacted");
    }
}
