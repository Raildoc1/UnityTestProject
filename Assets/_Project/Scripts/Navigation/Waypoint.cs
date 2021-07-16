using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Navigation
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _enemies;
        public bool NoEnemiesLeft()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i].activeInHierarchy)
                {
                    return false;
                }
            }
            return true;
        }
    }
}