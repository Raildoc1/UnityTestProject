using System.Collections;
using System.Collections.Generic;
using TestGame.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace TestGame.View
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private HealthManager _target;
        [SerializeField] private Image _fillImage;

        private void OnEnable()
        {
            if (!_target)
            {
                _target = GetComponentInParent<HealthManager>();
            }

            if (!_target)
            {
                Debug.LogError("HP Bar has no target!");
                gameObject.SetActive(true);
                return;
            }

            _target.OnHealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            if (!_target)
            {
                return;
            }

            _target.OnHealthChanged -= OnHealthChanged;
        }

        private void FixedUpdate()
        {
            transform.forward = (Camera.main.transform.position - transform.position).normalized;
        }

        private void OnHealthChanged(int current, int max)
        {
            _fillImage.fillAmount = current / (float)max;
        }
    }
}