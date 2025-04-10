using BricksBattle.Behaviors;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace BricksBattle.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private int PlayerLife = 3;
        [SerializeField]
        private Paddle paddle = null;
        [SerializeField]
        private BricksContainer bricksContainer = null;
        [SerializeField]
        private LoseZone loseZone = null;
        [SerializeField]
        private PopupsManager popupsManager = null;

        private InputAction startGameAction = null;
        private int currentScore = 0;
        private int currentLife = 0;
        private Rigidbody2D currentBall = null;

        public event Action<int> OnScoreChanged;
        public event Action<int> OnLifeChanged;

        public bool GameStarted { get; private set; } = false;

        private void Start()
        {
            startGameAction = InputSystem.actions.FindAction(Actions.StartGame);
            OnScoreChanged?.Invoke(currentScore);

            currentLife = PlayerLife;
            OnLifeChanged?.Invoke(currentLife);

            bricksContainer.OnBrickDestroyed += BricksContainer_OnBrickDestroyed;
            bricksContainer.OnAllBricksDestroyed += BricksContainer_OnAllBricksDestroyed;
            loseZone.OnBallLose += LoseZone_OnBallLose;
            paddle.OnLoseBallAnimationEnd += Paddle_OnLoseBallAnimationEnd;
        }

        private void Update()
        {
            if (!GameStarted && startGameAction.IsPressed())
            {
                StartGame();
            }
        }

        public void StartGame()
        {
            currentBall = paddle.BallLauncher.Launch();
            GameStarted = true;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public bool PaddlePointerMove
        {
            set => paddle.Movement.PaddlePointerMove = value;           
        }

        private void BricksContainer_OnBrickDestroyed(Brick brick)
        {
            currentScore += brick.Score;
            OnScoreChanged?.Invoke(currentScore);
        }

        private void BricksContainer_OnAllBricksDestroyed()
        {
            paddle.MovementEnabled = false;
            currentBall.simulated = false;
            popupsManager.ShowWinPopup();
        }

        private void LoseZone_OnBallLose()
        {   
            currentLife--;
            if (currentLife < 0)
            {
                paddle.MovementEnabled = false;
                popupsManager.ShowLosePopup();                
            }
            else
            {
                OnLifeChanged?.Invoke(currentLife);
                paddle.ProcessLoseBall();
            }
        }

        private void Paddle_OnLoseBallAnimationEnd()
        {
            GameStarted = false;
        }
    }
}
