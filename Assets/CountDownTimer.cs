using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    public TimerData timerData;

    private void Start()
    {
        timerData.StartTimer();
    }

    private void Update()
    {
        timerData.Tick(Time.deltaTime);
    }
}
