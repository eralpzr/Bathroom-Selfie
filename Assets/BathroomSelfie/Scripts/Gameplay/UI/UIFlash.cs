using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BathroomSelfie.Gameplay.UI
{
    public class UIFlash : MonoBehaviour
    {
        private Image _image;
        private Tweener _tweener;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void Flash(float duration)
        {
            _tweener?.Rewind();
            _tweener?.Kill();
            
            _image.color = new Color(1f, 1f, 1f, 1f);
            _tweener = _image.DOFade(0f, duration);
        }
    }
}