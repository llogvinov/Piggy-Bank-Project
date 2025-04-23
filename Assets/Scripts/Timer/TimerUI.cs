using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
    public class TimerUI : MonoBehaviour
    {
        public GameTimer _timer;
        public Text _timerText;

        private void Update()
        {
            if (_timer.IsRunning)
                UpdateTimerUI(_timer.TimeRemaining);
        }

        private void UpdateTimerUI(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.CeilToInt(time % 60f);
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}