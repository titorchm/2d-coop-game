using Services;
using UnityEngine;
using Zenject;

public class TestSetPlayerAppearance : MonoBehaviour
{
    [Inject] private PlayerAppearanceService _playerAppearanceService;
    
     void Start()
    {
        _playerAppearanceService.SetPlayerAppearancePayload(0,0,0,0);
    }
}
