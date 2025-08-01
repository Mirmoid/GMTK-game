using UnityEngine;
using UnityEngine.UI;

public class StaminaUIController : MonoBehaviour, IStaminaUI
{
    [Header("References")]
    [SerializeField] private Slider _staminaSlider;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Text _staminaText;

    [Header("Settings")]
    [SerializeField] private Color _fullStaminaColor = Color.cyan;
    [SerializeField] private Color _lowStaminaColor = Color.yellow;
    [SerializeField] private Color _exhaustedColor = Color.red;
    [SerializeField] private float _lowStaminaThreshold = 0.3f;

    public void UpdateStaminaUI(float currentStamina, float maxStamina)
    {
        _staminaSlider.maxValue = maxStamina;
        _staminaSlider.value = currentStamina;

        if (_staminaText != null)
            _staminaText.text = $"{currentStamina:F0}/{maxStamina:F0}";

        float staminaPercentage = currentStamina / maxStamina;

        if (currentStamina <= 0)
        {
            _fillImage.color = _exhaustedColor;
        }
        else if (staminaPercentage < _lowStaminaThreshold)
        {
            _fillImage.color = Color.Lerp(_exhaustedColor, _lowStaminaColor,
                staminaPercentage / _lowStaminaThreshold);
        }
        else
        {
            _fillImage.color = Color.Lerp(_lowStaminaColor, _fullStaminaColor,
                (staminaPercentage - _lowStaminaThreshold) / (1 - _lowStaminaThreshold));
        }
    }
}