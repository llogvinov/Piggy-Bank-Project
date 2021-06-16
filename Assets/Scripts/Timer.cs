using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerText;

    private float currentTime = 0f;
    private bool startNewTimer;
    private bool stopTimer;

    private void Start()
    {
        timerText = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (startNewTimer)
        {
            currentTime -= Time.deltaTime;
            timerText.text = currentTime.ToString("00");
        }

        if (currentTime <= 0)
        {
            if (!stopTimer)
            {
                StopTimer();
            }
        }
    }

    public void StartNewTimer(float duration)
    {
        currentTime = duration;
        startNewTimer = true;
        stopTimer = false;
        timerText.gameObject.SetActive(true);
    }

    private void StopTimer()
    {
        stopTimer = true;
        startNewTimer = false;
        timerText.gameObject.SetActive(false);
    }

}
