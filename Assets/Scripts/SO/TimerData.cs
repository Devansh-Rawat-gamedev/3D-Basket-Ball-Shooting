using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TimerData", menuName = "Scriptable Objects/TimerData")]
public class TimerData : ScriptableObject
{
    public float Duration = 10f;  
    public float TimeRemaining { get; private set; }

    public event Action<float> OnTimeUpdated;
    public UnityEvent OnTimeUp;

    public bool IsRunning { get; private set; }

    public void StartTimer()
    {
        TimeRemaining = Duration;
        IsRunning = true;
    }

    public void StopTimer()
    {
        IsRunning = false;
    }

    public void Tick(float deltaTime)
    {
        if (!IsRunning) return;

        TimeRemaining -= deltaTime;
        TimeRemaining = Mathf.Max(TimeRemaining, 0f);
        OnTimeUpdated?.Invoke(TimeRemaining);

        if (TimeRemaining <= 0f)
        {
            IsRunning = false;
            OnTimeUp?.Invoke();
        }
    }
}
