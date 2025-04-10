using BricksBattle.Mechanics;
using UnityEngine;

namespace BricksBattle.Behaviors
{
    [DefaultExecutionOrder(-50)] //to modify velocity by ConstantSpeedBounce mechanic before any other mechanics
    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private new Rigidbody2D rigidbody = null;

        private ConstantSpeedBounce constantBounce;

        private void Awake()
        {
            constantBounce = new ConstantSpeedBounce(rigidbody);
        }

        private void FixedUpdate()
        {
            constantBounce.SaveSpeed();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            constantBounce.ProcessCollision(collision);
        }
    }
}
