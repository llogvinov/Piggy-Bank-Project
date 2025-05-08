using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
    public class TimerUI : MonoBehaviour
    {
        public enum TimerFormat
        {
            SecondsOnly,
            MinutesSeconds
        }

        public TimerFormat _format = TimerFormat.SecondsOnly;
        public GameTimer _timer;
        public Text _timerText;

        private void Update()
        {
            if (_timer.IsRunning)
                UpdateTimerUI(_timer.TimeRemaining);
        }

        private void UpdateTimerUI(float time)
        {
            switch (_format)
            {
                case TimerFormat.SecondsOnly:
                    _timerText.text = Mathf.CeilToInt(time).ToString();
                    break;
                case TimerFormat.MinutesSeconds:
                    int minutes = Mathf.FloorToInt(time / 60f);
                    int seconds = Mathf.CeilToInt(time % 60f);
                    _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                    break;
                default:
                    Debug.LogError("Time format is invalid");
                    break;
            }
        }
    }
}