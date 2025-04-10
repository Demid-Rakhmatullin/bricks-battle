using BricksBattle.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BricksBattle.Views
{
    public class PaddleMoveArea : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
    {
        [SerializeField]
        private LevelManager levelManager;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!levelManager.GameStarted)
            {
                levelManager.StartGame();
            }
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            levelManager.PaddlePointerMove = true;
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            levelManager.PaddlePointerMove = false;
        }
    }
}
