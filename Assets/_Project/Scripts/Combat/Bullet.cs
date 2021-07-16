using UnityEngine;

namespace TestGame.Combat
{
    public class Bullet : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
        }
    }
}

