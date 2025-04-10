using BricksBattle.Managers;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace BricksBattle.Views
{
    public class Hud : MonoBehaviour
    {
        [SerializeField]
        private LevelManager levelManager = null;
        [SerializeField]
        private TMP_Text scoreValue = null;
        [SerializeField]
        private TMP_Text lifeValue = null;

        private void OnEnable()
        {
            levelManager.OnScoreChanged += LevelManager_OnScoreChanged;
            levelManager.OnLifeChanged += LevelManager_OnLifeChanged;
        }

        private void OnDisable()
        {
            levelManager.OnScoreChanged -= LevelManager_OnScoreChanged;
        }

        private void LevelManager_OnScoreChanged(int newScore)
        {
            scoreValue.text = newScore.ToString();
        }

        private void LevelManager_OnLifeChanged(int newLife)
        {
            lifeValue.text = newLife.ToString();
            lifeValue.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f), 1, 5, 0);
        }
    }
}
