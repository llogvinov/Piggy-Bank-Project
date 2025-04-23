using System;
using UnityEngine;

namespace Timer
{
    public class GameTimer : MonoBehaviour
    {
        public event Action TimerCompleted;

        public float TimeRemaining { get; private set; }
        public bool IsRunning { get; private set; } = false;

        private float _duration;

        public void SetTimer(int duration)
        {
            _duration = duration;
            TimeRemaining = _duration;
            IsRunning = true;
        }

        public void UnPauseTimer() => 
            IsRunning = true;

        public void PauseTimer() => 
            IsRunning = false;

        private void Update()
        {
            if (IsRunning)
            {
                TimeRemaining -= Time.deltaTime;
                if (TimeRemaining <= 0f)
                {
                    IsRunning = false;
                    TimerCompleted?.Invoke();
                }
            }
        }
    }
}