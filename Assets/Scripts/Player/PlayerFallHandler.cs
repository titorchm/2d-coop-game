using UnityEngine;

public class PlayerFallHandler : MonoBehaviour
{
    public GameEvent onTrigger;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTrigger.Raise(other.transform);
        }
    }
}
