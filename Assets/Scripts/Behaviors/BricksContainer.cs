using System;
using UnityEngine;

namespace BricksBattle.Behaviors
{
    public class BricksContainer : MonoBehaviour
    {
        public event Action<Brick> OnBrickDestroyed;
        public event Action OnAllBricksDestroyed;

        private int currentCount = -1;

        private void Awake()
        {
            Brick[] bricks = GetComponentsInChildren<Brick>();
            foreach (Brick brick in bricks)
                brick.Init(this);

            currentCount = bricks.Length;
        }
        
        public void DestroyBrick(Brick brick)
        {
            currentCount--;
            OnBrickDestroyed?.Invoke(brick);

            if (currentCount == 0)
                OnAllBricksDestroyed?.Invoke();
        }
    }
}
