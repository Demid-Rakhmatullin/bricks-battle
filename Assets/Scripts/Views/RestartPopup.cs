using BricksBattle.Managers;
using UnityEngine;

namespace BricksBattle.Views
{
    public class RestartPopup : MonoBehaviour
    {
        [SerializeField]
        private LevelManager levelManager = null;

        public void Restart()
        {
            levelManager.RestartLevel();
        }
    }
}
