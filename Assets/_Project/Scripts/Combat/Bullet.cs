using TestGame.Enemy;
using UnityEngine;

namespace TestGame.Combat
{
    public class Bullet : MonoBehaviour
    {
        private float _timeLeft;

        [SerializeField] private float _speed = 10f;
        [SerializeField] private int _damage = 40;
        [SerializeField] private float _lifeTime = 3f;

        private void OnEnable()
        {
            _timeLeft = _lifeTime;
        }

        private void Update()
        {
            _timeLeft -= Time.deltaTime;
            transform.position += _speed * Time.deltaTime * transform.forward;

            if (_timeLeft < 0f)
            {
                Destroy();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                var health = other.gameObject.GetComponent<HealthManager>();
                health.ApplyDamage(_damage);
                Destroy();
            }
        }

        private void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}

