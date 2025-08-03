using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private Text timerText;

    private RainController rainController;

    private void Start()
    {
        rainController = FindObjectOfType<RainController>();
        if (rainController == null)
        {
            Debug.LogError("RainController not found in scene!");
        }
    }

    private void Update()
    {
        if (timerText == null || rainController == null) return;

        if (!rainController.IsRaining)
        {
            float timeRemaining = rainController.TimeRemaining;
            timerText.text = FormatTime(timeRemaining);
        }
        else
        {
            timerText.text = "RAINING!";
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        int milliseconds = Mathf.FloorToInt((timeInSeconds * 1000f) % 1000f);
        return $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
}