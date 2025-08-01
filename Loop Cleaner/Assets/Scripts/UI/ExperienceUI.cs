using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerExperience))]
public class ExperienceUI : MonoBehaviour
{
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _expText;
    [SerializeField] private Slider _expSlider;

    private IExperienceGainer _experience;

    public void Initialize(IExperienceGainer experience)
    {
        _experience = experience;
        UpdateUI();
    }

    public void UpdateUI()
    {
        _levelText.text = $"Level {_experience.CurrentLevel}";
        _expText.text = $"{_experience.CurrentExp}/{_experience.ExpToNextLevel}";
        _expSlider.maxValue = _experience.ExpToNextLevel;
        _expSlider.value = _experience.CurrentExp;
    }
}