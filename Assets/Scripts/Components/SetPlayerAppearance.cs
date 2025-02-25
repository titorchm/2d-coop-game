using Services;
using UnityEngine;
using Zenject;

public class SetPlayerAppearance : MonoBehaviour
{
    [Inject] PlayerAppearanceService _playerAppearanceService;
    
    void Start()
    {
        _playerAppearanceService.SetPlayerAppearancePayload(0,0,0,0);
    }
}
