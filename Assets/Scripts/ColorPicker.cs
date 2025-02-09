using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorPicker : MonoBehaviour
{
    [Header("Sliders")] public Slider hueSlider;
    public Slider saturationSlider;
    public Slider lightnessSlider;

    [Header("Character")] public SpriteRenderer characterColor;

    [Header("Slider Fills")] public Image saturationFill;
    public Image lightnessFill;

    [Header("Slider Handles")] public Image hueHandle;
    public Image saturationHandle;
    public Image lightnessHandle;

    private float hue = 0f;
    private float saturation = 1f;
    private float lightness = 0.5f;

    void Start()
    {
        // Initialize sliders
        hueSlider.value = hue;
        saturationSlider.value = saturation;
        lightnessSlider.value = lightness;

        // Add listeners
        hueSlider.onValueChanged.AddListener(OnHueChanged);
        saturationSlider.onValueChanged.AddListener(OnSaturationChanged);
        lightnessSlider.onValueChanged.AddListener(OnLightnessChanged);

        UpdateColor();
    }

    void OnHueChanged(float value)
    {
        hue = value;
        UpdateColor();
        UpdateSliderVisuals();
    }

    void OnSaturationChanged(float value)
    {
        saturation = value;
        UpdateColor();
        UpdateSliderVisuals();
    }

    void OnLightnessChanged(float value)
    {
        lightness = value;
        UpdateColor();
        UpdateSliderVisuals();
    }

    void UpdateColor()
    {
        Color newColor = Color.HSVToRGB(hue, saturation, lightness);
        characterColor.color = newColor;

        // Update handle colors
        hueHandle.color = Color.HSVToRGB(hue, 1f, 1f);
        saturationHandle.color = Color.HSVToRGB(hue, saturation, lightness);
        lightnessHandle.color = Color.HSVToRGB(hue, saturation, lightness);
    }

    void UpdateSliderVisuals()
    {
        // Update saturation slider background
        if (saturationFill != null)
        {
            Gradient satGradient = new Gradient();
            GradientColorKey[] colorKeys = new GradientColorKey[2];
            colorKeys[0] = new GradientColorKey(Color.HSVToRGB(hue, 0f, lightness), 0f);
            colorKeys[1] = new GradientColorKey(Color.HSVToRGB(hue, 1f, lightness), 1f);

            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
            alphaKeys[0] = new GradientAlphaKey(1f, 0f);
            alphaKeys[1] = new GradientAlphaKey(1f, 1f);

            satGradient.SetKeys(colorKeys, alphaKeys);
            saturationFill.material.SetFloat("_Hue", hue);
        }

        // Update lightness slider background
        if (lightnessFill != null)
        {
            Gradient lightGradient = new Gradient();
            GradientColorKey[] colorKeys = new GradientColorKey[3];
            colorKeys[0] = new GradientColorKey(Color.black, 0f);
            colorKeys[1] = new GradientColorKey(Color.HSVToRGB(hue, saturation, 0.5f), 0.5f);
            colorKeys[2] = new GradientColorKey(Color.white, 1f);

            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[3];
            alphaKeys[0] = new GradientAlphaKey(1f, 0f);
            alphaKeys[1] = new GradientAlphaKey(1f, 0.5f);
            alphaKeys[2] = new GradientAlphaKey(1f, 1f);

            lightGradient.SetKeys(colorKeys, alphaKeys);
            lightnessFill.material.SetFloat("_Saturation", saturation);
        }
    }
}