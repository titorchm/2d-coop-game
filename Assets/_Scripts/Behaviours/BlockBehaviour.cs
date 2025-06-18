using System.Collections;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public interface ICollectable
{
    IItem Item { get; }
    
    public void Collect();
    public void CancelCollect();
}

public interface IInteractable
{
    // leave as is, just not implemented anywhere
    public void Interact();
}

public class BlockBehaviour : NetworkBehaviour, ICollectable
{
    public BlockData blockData;

    public IItem Item => blockData;
    
    [SerializeField] private ParticleSystem earthParticles;
    
    [SerializeField] private GameEvent onItemCollected;
    
    private SpriteRenderer _spriteRenderer;
    
    private bool isCollecting = false;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = Item.Sprite;
    }
    
    //[Rpc(SendTo.Server)]
    public void Collect()
    {
        isCollecting = true;
        
        StartCoroutine(StartCollecting());
    }
    
    public void CancelCollect()
    {
        isCollecting = false;
    }

    [Rpc(SendTo.Everyone)]
    private void StartParticlesRpc()
    {
        earthParticles.Play();
    }

    [Rpc(SendTo.Everyone)]
    private void StopParticlesRpc()
    {
        earthParticles.Stop();
    }
    
    private IEnumerator StartCollecting()
    {
        float timer = 0f;

        StartParticlesRpc();
    
        while (isCollecting && timer < 1f)
        {
            timer += Time.deltaTime;
        
            yield return null;
        }

        if (!isCollecting)
        {
            StopParticlesRpc();
        
            yield break;
        }

        _spriteRenderer.sprite = null;
        
        yield return new WaitForSeconds(earthParticles.main.duration - earthParticles.time);
        
        StopParticlesRpc();

        SendEventRpc();
        
        isCollecting = false;
    }

    [Rpc(SendTo.Server)]
    private void SendEventRpc()
    {
        onItemCollected.Raise(gameObject);
    }
}
