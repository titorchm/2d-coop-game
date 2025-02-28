using System.Collections;
using UnityEngine;

public interface ICollectable
{
    public void Collect();
    public void CancelCollect();
}

public interface IInteractable
{
    public void Interact();
}

public class BlockBehaviour : MonoBehaviour, ICollectable
{
    public BlockData blockData;
    
    [SerializeField] private ParticleSystem earthParticles;
    
    private SpriteRenderer _spriteRenderer;
    
    private bool isCollecting = false;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = blockData.Sprite;
    }

    public void Collect()
    {
        isCollecting = true;
        
        StartCoroutine(StartCollecting());
    }
    
    public void CancelCollect()
    {
        isCollecting = false;
    }

    private IEnumerator StartCollecting()
    {
        float timer = 0f;

        earthParticles.Play();
    
        while (isCollecting && timer < 1f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (!isCollecting)
        {
            earthParticles.Stop();
            yield break;
        }
        
        _spriteRenderer.sprite = null;
    
        earthParticles.Stop();

        yield return new WaitForSeconds(earthParticles.time);
        
        Destroy(gameObject);
    
        isCollecting = false;
    }

}
