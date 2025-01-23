using UnityEngine;

public class GenerateGround : MonoBehaviour
{
    [SerializeField] private GameObject groundPrefab;

    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            Instantiate(groundPrefab, new Vector2(i, -5f), Quaternion.identity);
        }
    }
}
