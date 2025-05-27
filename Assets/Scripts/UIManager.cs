using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    public PlayerData playerData;
    public TimerData timerData;
    public TextMeshProUGUI shotsText;
    public TextMeshProUGUI countDownText;

    private void Start()
    {
        playerData.OnShot += UpdateShotsText;
        timerData.OnTimeUpdated += UpdateCountDownText;
        UpdateShotsText();
    }

    private void UpdateCountDownText(float obj)
    {
        countDownText.text = $"Time: {Mathf.CeilToInt(obj)}s";
    }

    public void UpdateShotsText()
    {
        shotsText.text = $"Shots: {playerData.ShotsMade} / {playerData.ShotsTaken}";
    }

    private void OnDestroy()
    {
        playerData.OnShot -= UpdateShotsText;
        timerData.OnTimeUpdated -= UpdateCountDownText;
    }
}
