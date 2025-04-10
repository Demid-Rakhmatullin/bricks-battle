using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace BricksBattle.Managers
{
    public class PopupsManager : MonoBehaviour
    {
        [SerializeField]
        private Image overlay = null;

        [SerializeField]
        private RectTransform winPopup = null;

        [SerializeField]
        private RectTransform losePopup = null;

        private void Awake()
        {
            overlay.enabled = false;
            winPopup.gameObject.SetActive(false);
            losePopup.gameObject.SetActive(false);
        }

        private void ShowPopup(RectTransform popup)
        {
            overlay.enabled = true;
            overlay
                .DOFade(246f / 255, 2)
                .From(0)
                .OnComplete(() => 
                    {
                        popup.gameObject.SetActive(true);
                        popup.DOShakeAnchorPos(1, 30);
                    }
                );
        }

        public void ShowWinPopup()
            => ShowPopup(winPopup);

        public void ShowLosePopup()
            => ShowPopup(losePopup);
    }
}
