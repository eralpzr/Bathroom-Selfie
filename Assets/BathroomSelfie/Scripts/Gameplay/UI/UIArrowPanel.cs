﻿using System;
using System.Collections.Generic;
using BathroomSelfie.Enums;
using BathroomSelfie.Manager;
using BathroomSelfie.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BathroomSelfie.Gameplay.UI
{
    public sealed class UIArrowPanel : UIObject
    {
        [SerializeField] private UIArrow _arrowPrefab;
        [SerializeField] private RectTransform _boxTransform;
        [SerializeField] private Transform _arrowRoot;

        private readonly List<UIArrow> _arrows = new List<UIArrow>();

        private Image _boxImage;
        private Tweener _boxTweener;
        
        public int ArrowCount => _arrows.Count;
        public UIArrow SelectedArrow { get; private set; }

        private void Awake()
        {
            _boxImage = _boxTransform.GetComponent<Image>();
        }

        public UIArrow CreateArrow(ArrowDirection direction)
        {
            var arrow = Instantiate(_arrowPrefab, _arrowRoot);
            arrow.Direction = direction;
            
            arrow.rectTransform.anchoredPosition = new Vector2(Screen.width / 2f + 80, 0f);

            var tweener = arrow.rectTransform.DOAnchorPos(new Vector2(-( Screen.width / 2f + 80 ), 0f), GameManager.Instance.gameData.arrowMoveDuration).SetEase(Ease.Linear);
            tweener.onComplete = () => Destroy(arrow.gameObject);
            tweener.onUpdate = () =>
                               {
                                   if (SelectedArrow != null && SelectedArrow != arrow)
                                       return;

                                   SelectedArrow = IsInBox(arrow.rectTransform) ? arrow : null;
                               };

            _arrows.Add(arrow);
            return arrow;
        }

        public void DestroyArrow(UIArrow arrow)
        {
            _arrows.Remove(arrow);
        }

        public void HighlightBox(Color color)
        {
            _boxTweener?.Rewind();
            _boxTweener?.Kill();
            
            _boxImage.color = color;
            _boxTweener = _boxImage.DOColor(Color.white, .75f);
        }

        private bool IsInBox(RectTransform rectTransform)
        {
            return rectTransform.anchoredPosition.x < _boxTransform.anchoredPosition.x + _boxTransform.sizeDelta.x / 2f
                   && rectTransform.anchoredPosition.x > _boxTransform.anchoredPosition.x - _boxTransform.sizeDelta.x / 2f;
        }
    }
}