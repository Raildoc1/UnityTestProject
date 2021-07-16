using UnityEngine;

namespace TestGame.Enemy
{
    public class HealthManager : MonoBehaviour
    {
        private int _health = 0;

        [SerializeField] private int _maxHealth = 100;

        public bool IsDead => Health == 0;

        public int Health
        {
            get => _health;
            private set
            {
                var health = Mathf.Clamp(value, 0, _maxHealth);
                if (_health != health)
                {
                    OnHealthChanged?.Invoke(health, _maxHealth);
                    _health = health;
                }

                if (_health <= 0)
                {
                    Die();
                }
            }
        }

        public delegate void OnIntPropertyChangedEvent(int value, int maxHealth);
        public event OnIntPropertyChangedEvent OnHealthChanged;

        private void Start()
        {
            Health = _maxHealth;
        }

        public void ApplyDamage(int damage)
        {
            if (damage < 0)
            {
                Debug.LogError("Damage can't be negative!");
                return;
            }

            Health -= damage;
        }

        private void Die()
        {
            var animator = GetComponent<Animator>();

            if (animator)
            {
                animator.enabled = false;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
