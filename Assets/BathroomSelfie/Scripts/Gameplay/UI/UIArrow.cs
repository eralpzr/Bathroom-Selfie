using System;
using BathroomSelfie.Enums;
using BathroomSelfie.Manager;
using BathroomSelfie.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BathroomSelfie.Gameplay.UI
{
    public sealed class UIArrow : UIObject
    {
        [HideInInspector] public RectTransform rectTransform;
        private ArrowDirection _direction;

        private Image _image;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            rectTransform = GetComponent<RectTransform>();
        }

        public void FadeOutDestroy()
        {
            rectTransform.DOKill();

            var seq = DOTween.Sequence();
            seq.Join(rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + 160f, .75f));
            seq.Join(_image.DOFade(0f, .75f));
            seq.onComplete = () =>
                             {
                                 Destroy(gameObject);
                             };
        }

        private void OnDisable()
        {
            UIManager.Instance.arrowPanel.DestroyArrow(this);

            if (UIManager.Instance.arrowPanel.ArrowCount == 0)
            {
                GameManager.Instance.StartCoroutine(GameManager.Instance.CompleteLevelCoroutine(false));
            }
        }

        public ArrowDirection Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                transform.eulerAngles = new Vector3(0f, 0f, -90f * (int) _direction);
            }
        }
    }
}