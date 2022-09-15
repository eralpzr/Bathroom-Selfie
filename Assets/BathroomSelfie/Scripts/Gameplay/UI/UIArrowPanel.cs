using System;
using BathroomSelfie.Enums;
using BathroomSelfie.Manager;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BathroomSelfie.Gameplay.UI
{
    public sealed class UIArrowPanel : MonoBehaviour
    {
        [SerializeField] private UIArrow _arrowPrefab;
        [SerializeField] private RectTransform _boxTransform;
        [SerializeField] private Transform _arrowRoot;

        private Image _boxImage;
        private Tweener _boxTweener;
        
        public UIArrow SelectedArrow { get; private set; }

        private void Awake()
        {
            _boxImage = _boxTransform.GetComponent<Image>();
        }

        public UIArrow CreateArrow(ArrowDirection direction)
        {
            var arrow = Instantiate(_arrowPrefab, _arrowRoot);
            arrow.Direction = direction;
            
            var rectTransform = arrow.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(Screen.width / 2f + 80, 0f);

            var tweener = rectTransform.DOAnchorPos(new Vector2(-( Screen.width / 2f + 80 ), 0f), GameManager.Instance.gameData.arrowMoveDuration).SetEase(Ease.Linear);
            tweener.onComplete = () => Destroy(arrow.gameObject);
            tweener.onUpdate = () =>
                               {
                                   if (SelectedArrow != null && SelectedArrow != arrow)
                                       return;

                                   SelectedArrow = IsInBox(rectTransform) ? arrow : null;
                               };

            return arrow;
        }

        public void HighlightBox(Color color)
        {
            _boxTweener?.Rewind();
            _boxTweener?.Kill();
            
            _boxImage.color = color;
            _boxTweener = _boxImage.DOColor(Color.white, .5f);
        }

        private bool IsInBox(RectTransform rectTransform)
        {
            return rectTransform.anchoredPosition.x < _boxTransform.anchoredPosition.x + _boxTransform.sizeDelta.x / 2f
                   && rectTransform.anchoredPosition.x > _boxTransform.anchoredPosition.x - _boxTransform.sizeDelta.x / 2f;
        }
    }
}