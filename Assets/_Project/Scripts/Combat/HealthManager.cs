using UnityEngine;

namespace TestGame.Enemy
{
    public class HealthManager : MonoBehaviour
    {
        private int _health = 0;

        [SerializeField] private int _maxHealth = 100;

        public int Health
        {
            get => _health;
            set
            {
                var health = Mathf.Clamp(value, 0, _maxHealth);
                if (_health != health)
                {
                    OnHealthChanged?.Invoke(_health, health);
                    _health = health;
                }

                if (_health <= 0)
                {
                    OnPlayerDie?.Invoke();
                }
            }
        }

        public delegate void OnIntPropertyChangedEvent(int oldValue, int newValue);
        public event OnIntPropertyChangedEvent OnHealthChanged;

        public delegate void OnPlayerDieEvent();
        public event OnPlayerDieEvent OnPlayerDie;

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"{collision}");
        }

    }
}
