using System;
using UnityEngine;

namespace BricksBattle.Mechanics
{
    /// <summary>
    /// Кастомная физика отскока мяча от платформы. Угол отскока зависит от того, в какое место, относительно центра платформы, попал мяч.
    /// Позволяет игроку управлять мячом, отбивая его тем или иным краем платформы.
    /// </summary>
    public class PaddleBallBounce
    {
        [Serializable]
        public class Settings
        {
            public float maxAngle = 75f;
        }

        private readonly Transform transform;
        private readonly float halfWidth;
        private readonly float heightDeltaThreshold; 
        private readonly Settings settings;

        public PaddleBallBounce(Collider2D collider, Settings settings)
        {
            transform = collider.transform;
            halfWidth = collider.bounds.extents.x;
            heightDeltaThreshold = collider.bounds.extents.y - Mathf.Epsilon;
            this.settings = settings;
        }

        public void Process(Collision2D ballCollision)
        {
            Vector2 contactPoint = ballCollision.GetContact(0).point;

            //if contact point below a certain point - don't use algorithm to prevent "paddle side edge hack"
            if (contactPoint.y < transform.position.y + heightDeltaThreshold)
            {
                return;
            }

            float absoluteOffset = transform.position.x - contactPoint.x;
            float relativeOffset = absoluteOffset / halfWidth; // percent coefficient in [-1 .. 1] range
            float angleDelta = relativeOffset * settings.maxAngle; //calculated delta between contact and bounce angles (in degrees)

            Vector2 contactVelocity = ballCollision.rigidbody.linearVelocity;
            float contactAngle = Vector2.SignedAngle(Vector2.up, contactVelocity);
            float bounceAngle = Mathf.Clamp(contactAngle + angleDelta,
                -settings.maxAngle, settings.maxAngle);
            Vector2 bounceDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward)
                * Vector2.up;

            ballCollision.rigidbody.linearVelocity = bounceDirection * contactVelocity.magnitude;
        }
    }
}
