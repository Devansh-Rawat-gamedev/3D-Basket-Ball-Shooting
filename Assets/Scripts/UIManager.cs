using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [FormerlySerializedAs("playerData")] 
    public ScoreData scoreData;
    public TimerData timerData;
    public TextMeshProUGUI shotsText;
    public TextMeshProUGUI countDownText;

    private void Start()
    {
        scoreData.OnShot += UpdateShotsText;
        timerData.OnTimeUpdated += UpdateCountDownText;
        UpdateShotsText();
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void UpdateCountDownText(float obj)
    {
        countDownText.text = $"Time: {Mathf.CeilToInt(obj)}s";
    }

    private void UpdateShotsText(int attempts = 0, int made = 0)
    {
        shotsText.text = $"Shots: {scoreData.ShotsMade} / {scoreData.ShotsTaken}";
    }

    private void OnDestroy()
    {
        scoreData.OnShot -= UpdateShotsText;
        timerData.OnTimeUpdated -= UpdateCountDownText;
    }
}
