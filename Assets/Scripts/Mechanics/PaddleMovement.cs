using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace BricksBattle.Mechanics
{
    public class PaddleMovement
    {
        [Serializable]
        public class Settings
        {
            public float keyboardSpeed = 30f;
            public float pointerSpeed = 2f;
        }

        private readonly Rigidbody2D rigidbody;
        private readonly Settings settings;
        private readonly InputAction moveAction;
        private Vector2 move;

        public bool PaddlePointerMove { private get; set; }

        public PaddleMovement(Rigidbody2D rigidbody, Settings settings)
        { 
            this.rigidbody = rigidbody;
            this.settings = settings;
            moveAction = InputSystem.actions.FindAction(Actions.PaddleMove);
            move = Vector2.zero;
            PaddlePointerMove = false;
        }

        public void CheckInput()
        {
            float moveInput = moveAction.ReadValue<float>();
            bool isMoving = false;

            if (moveInput != 0f)
            {
                if (moveAction.activeControl is KeyControl)
                {
                    move = new Vector2(moveInput * settings.keyboardSpeed, 0f);
                    isMoving = true;
                }
                else if (PaddlePointerMove)
                {
                    move = new Vector2(moveInput * settings.pointerSpeed, 0f);
                    isMoving = true;
                }
            }

            if (!isMoving)
            {
                move = Vector2.zero;
            }
        }

        public void Process()
        {
            if (move != Vector2.zero)
            {
                rigidbody.AddForce(move);
            }
        } 
    }
}
