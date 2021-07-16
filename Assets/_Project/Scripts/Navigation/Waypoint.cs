using System.Collections.Generic;
using TestGame.Enemy;
using UnityEngine;

namespace TestGame.Navigation
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private List<HealthManager> _enemies;
        public bool NoEnemiesLeft()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (!_enemies[i].IsDead)
                {
                    return false;
                }
            }
            return true;
        }
    }
}