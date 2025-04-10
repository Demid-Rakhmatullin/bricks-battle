using BricksBattle.Mechanics;
using System;
using System.Collections;
using UnityEngine;

namespace BricksBattle.Behaviors
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField]
        private new Collider2D collider = null;

        [SerializeField]
        private new Rigidbody2D rigidbody = null;

        [SerializeField]
        private SpriteRenderer spriteRenderer = null;

        [SerializeField]
        private PaddleMovement.Settings movementSettings = new();

        [SerializeField, Tooltip("Мяч, запускаемый на старте игры")]
        private BallLauncher.Settings initialBallSettings = new();

        [SerializeField, Tooltip("Отскок мяча от платформы")]
        private PaddleBallBounce.Settings ballBounceSettings = new();

        public PaddleMovement Movement { get; private set; }
        public BallLauncher BallLauncher { get; private set; }
        public bool MovementEnabled { get; set; }

        public event Action OnLoseBallAnimationEnd;

        private PaddleBallBounce ballBounce = null;
        private YieldInstruction loseAnimationDelay = null;

        private void Awake()
        {
            Movement = new PaddleMovement(rigidbody, movementSettings);
            BallLauncher = new BallLauncher(collider, initialBallSettings);
            ballBounce = new PaddleBallBounce(collider, ballBounceSettings);
            MovementEnabled = true;
            loseAnimationDelay = new WaitForSeconds(0.3f);
        }

        private void Update()
        {
            if (MovementEnabled)
                Movement.CheckInput();
        }

        private void FixedUpdate()
        {
            if (MovementEnabled)
                Movement.Process();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(Tags.Ball))
            {
                ballBounce.Process(collision);
            }
        }

        public void ProcessLoseBall()
        {
            StartCoroutine(LoseBallAnimation());
        }

        private IEnumerator LoseBallAnimation()
        {
            MovementEnabled = false;

            for (int i = 0; i < 3; i++)
            {
                spriteRenderer.enabled = false;
                yield return loseAnimationDelay;

                spriteRenderer.enabled = true;
                yield return loseAnimationDelay;
            }

            MovementEnabled = true;
            OnLoseBallAnimationEnd?.Invoke();
        }
    }
}
