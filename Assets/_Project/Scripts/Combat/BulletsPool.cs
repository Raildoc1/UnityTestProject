using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Combat
{
    public class BulletsPool : MonoBehaviour
    {
        #region Singleton
        private static BulletsPool _instance;
        public static BulletsPool Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<BulletsPool>();
                }
                return _instance;
            }
        }
        #endregion

        private List<GameObject> _objectsPool;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private int _poolSize = 5;

        private void Start()
        {
            _objectsPool = new List<GameObject>();

            GameObject temp;
            for (int i = 0; i < _poolSize; i++)
            {
                temp = Instantiate(_bulletPrefab);
                temp.SetActive(false);
                _objectsPool.Add(temp);
            }
        }

        public GameObject GetBullet()
        {
            for (int i = 0; i < _objectsPool.Count; i++)
            {
                if (!_objectsPool[i].activeInHierarchy)
                {
                    _objectsPool[i].SetActive(true);
                    return _objectsPool[i];
                }
            }

            GameObject temp = Instantiate(_bulletPrefab);
            _objectsPool.Add(temp);
            return temp;
        }
    }
}

