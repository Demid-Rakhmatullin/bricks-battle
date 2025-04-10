using System;
using UnityEngine;

namespace BricksBattle.Behaviors
{
    public class LoseZone : MonoBehaviour
    {
        public event Action OnBallLose;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Ball))
            {
                OnBallLose?.Invoke();
                Destroy(collision.gameObject);
            }
        }
    }
}
