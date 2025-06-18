using Unity.Netcode;
using UnityEngine;

public class PlayerInteraction : NetworkBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private ICollectable _lastCollectedBlock;
    
    void OnEnable()
    {
        playerInput.OnInteract += OnPlayerInteract;
        playerInput.OnCollectStarted += OnPlayerCollect;
        playerInput.OnCollectCanceled += OnCollectCanceled;
    }

    private void OnDisable()
    {
        playerInput.OnInteract -= OnPlayerInteract;
        playerInput.OnCollectStarted -= OnPlayerCollect;
        playerInput.OnCollectCanceled -= OnCollectCanceled;
    }

    public void DespawnCollectedObject(object data)
    {
        GameObject obj = (GameObject)data;
        NetworkObject networkObject = obj.GetComponent<NetworkObject>();
        ulong id = networkObject.NetworkObjectId;
        
        DespawnCollectedObjectRpc(id);
    }
    
    private void OnPlayerInteract()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        
        if (hit.collider.TryGetComponent(out IInteractable objectToInteractWith))
        {
            objectToInteractWith.Interact();
        }
    }

    private void OnPlayerCollect()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        float interactionDistance = ((Vector2)gameObject.transform.position - hit.point).magnitude;
        
        if (interactionDistance > 5f) return;
        
        if (hit.collider.TryGetComponent(out ICollectable objectToCollect))
        {
            _lastCollectedBlock = objectToCollect;
            objectToCollect.Collect();
        }
    }

    private void OnCollectCanceled()
    {
        if (_lastCollectedBlock != null)
        {
            _lastCollectedBlock.CancelCollect();
        }
    }

    [Rpc(SendTo.Server)]
    private void DespawnCollectedObjectRpc(ulong objectId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(objectId, out NetworkObject objectToDespawn))
        {
            objectToDespawn.Despawn();
        }
        else
        {
            Debug.LogWarning($"DespawnCollectedObjectRpc: Object with ID {objectId} not found.");
        }
    }

}
