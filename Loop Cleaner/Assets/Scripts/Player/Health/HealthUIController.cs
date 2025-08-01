using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour, IHealthUI
{
    [Header("References")]
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Text _healthText;

    [Header("Settings")]
    [SerializeField] private Color _fullHealthColor = Color.green;
    [SerializeField] private Color _lowHealthColor = Color.red;
    [SerializeField] private float _lowHealthThreshold = 0.3f;

    public void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = currentHealth;

        if (_healthText != null)
        {
            _healthText.text = $"{currentHealth:F0}/{maxHealth:F0}";
        }

        float healthPercentage = currentHealth / maxHealth;
        _fillImage.color = Color.Lerp(_lowHealthColor, _fullHealthColor, healthPercentage / _lowHealthThreshold);
    }
}