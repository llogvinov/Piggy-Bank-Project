using System;
using UnityEngine;

namespace Main
{
    public class PlayerHealth : MonoBehaviour
    {
        public Action<PlayerHealth> HealthChanged;

        private int _health;

        public int Health
        {
            get => _health;
            set
            {
                _health = Mathf.Clamp(value, 0, MAX_HEALTH);
                HealthChanged?.Invoke(this);
            }
        }

        public const int MAX_HEALTH = 3;

        public void SetInitialHealth(int value = MAX_HEALTH) =>
            Health = value;

        public void TakeDamage(int damage) =>
            Health -= damage;

        public void AddHeart() =>
            Health++;
    }
}