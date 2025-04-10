using System;
using UnityEngine;

namespace BricksBattle.Mechanics
{
    public class BallLauncher
    {
        [Serializable]
        public class Settings
        {
            public Collider2D ballPrefab = null;
            public float startSpeed = 500f;
            [Tooltip("Отклонение относительно направления вертикально вверх. Если 0, то мяч будет запущен вертикально вверх от платформы.")]
            public float startAngle = 20f;
        }

        private readonly Collider2D paddle;
        private readonly Settings settings;

        public BallLauncher(Collider2D paddle, Settings settings)
        {
            this.paddle = paddle;
            this.settings = settings;
        }

        public Rigidbody2D Launch()
        {
            Collider2D ballCollider = GameObject.Instantiate(settings.ballPrefab);
            Vector2 startPosition = new(paddle.bounds.center.x,
                paddle.bounds.max.y + ballCollider.bounds.extents.y);
            ballCollider.transform.position = startPosition;

            Vector2 direction = Quaternion.AngleAxis(settings.startAngle, Vector3.forward) 
                * Vector2.up;
            Rigidbody2D ballRigidbody = ballCollider.attachedRigidbody;
            ballRigidbody.AddForce(direction * settings.startSpeed, 
                ForceMode2D.Impulse);

            return ballRigidbody;
        }
    }
}
