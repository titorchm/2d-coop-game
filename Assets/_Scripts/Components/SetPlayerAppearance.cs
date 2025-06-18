using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SetPlayerAppearance : MonoBehaviour
{
    [SerializeField] private Slider bodySlider;
    [SerializeField] private Slider eyesSlider;
    [SerializeField] private Slider faceSlider;
    [SerializeField] private Slider hatSlider;
    
    [Inject] PlayerAppearanceService _playerAppearanceService;

    private float bodyValue;
    private float eyesValue;
    private float faceValue;
    private float hatValue;
    
    void Start()
    {
        _playerAppearanceService.SetPlayerAppearancePayload(0,0,0,0);
    }

    private void OnEnable()
    {
        bodySlider.onValueChanged.AddListener(SetBodyValue);
        eyesSlider.onValueChanged.AddListener(SetEyesValue);
        faceSlider.onValueChanged.AddListener(SetFaceValue);
        hatSlider.onValueChanged.AddListener(SetHatValue);
    }

    private void OnDisable()
    {
        bodySlider.onValueChanged.RemoveListener(SetBodyValue);
        eyesSlider.onValueChanged.RemoveListener(SetEyesValue);
        faceSlider.onValueChanged.RemoveListener(SetFaceValue);
        hatSlider.onValueChanged.RemoveListener(SetHatValue);
    }

    private void SetAppearance(float body, float face, float eyes, float hat)
    {
        _playerAppearanceService.SetPlayerAppearancePayload(
            (int)body,
            (int)face,
            (int)eyes,
            (int)hat
        );
    }

    private void SetBodyValue(float body)
    {
        bodyValue = body;
        
        SetAppearance(bodyValue, faceValue, eyesValue, hatValue);
    }
    
    private void SetEyesValue(float eyes)
    {
        eyesValue = eyes;
        
        SetAppearance(bodyValue, faceValue, eyesValue, hatValue);
    }
    
    private void SetFaceValue(float face)
    {
        faceValue = face;
        
        SetAppearance(bodyValue, faceValue, eyesValue, hatValue);
    }
    
    private void SetHatValue(float hat)
    {
        hatValue = hat;
        
        SetAppearance(bodyValue, faceValue, eyesValue, hatValue);
    }
}
