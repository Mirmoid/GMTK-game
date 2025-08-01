using UnityEngine;

[RequireComponent(typeof(ExperienceUI))]
public class PlayerExperience : MonoBehaviour, IExperienceGainer
{
    [SerializeField] private int[] _levelThresholds = { 100, 250, 450, 700, 1000 };

    public int CurrentExp { get; private set; }
    public int CurrentLevel { get; private set; } = 1;
    public int ExpToNextLevel =>
        (CurrentLevel - 1 < _levelThresholds.Length) ? _levelThresholds[CurrentLevel - 1] : int.MaxValue;

    private ExperienceUI _experienceUI;

    private void Awake()
    {
        _experienceUI = GetComponent<ExperienceUI>();
        _experienceUI.Initialize(this);
    }

    public void GainExperience(int amount)
    {
        if (amount <= 0) return;

        CurrentExp += amount;
        _experienceUI.UpdateUI();

        while (CurrentLevel - 1 < _levelThresholds.Length &&
               CurrentExp >= _levelThresholds[CurrentLevel - 1])
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        CurrentExp -= _levelThresholds[CurrentLevel - 1];
        CurrentLevel++;
        _experienceUI.UpdateUI();
        CardSelectionManager.Instance.ShowCardSelection();
    }
}