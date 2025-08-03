public interface IDeathHandler
{
    void HandleDeath();
    event System.Action OnDeath;
}