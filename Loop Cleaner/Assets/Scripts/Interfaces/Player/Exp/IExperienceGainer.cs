public interface IExperienceGainer
{
    void GainExperience(int amount);
    int CurrentExp { get; }
    int CurrentLevel { get; }
    int ExpToNextLevel { get; }
}