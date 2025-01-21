using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    void Start()
    {
        playerInput.OnInteract += OnPlayerInteract;
    }

    private void OnPlayerInteract()
    {
        Debug.Log("Interacted");
    }
}
