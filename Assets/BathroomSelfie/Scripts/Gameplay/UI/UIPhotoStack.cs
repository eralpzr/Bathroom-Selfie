using System.Collections.Generic;
using BathroomSelfie.Manager;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BathroomSelfie.Gameplay.UI
{
    public class UIPhotoStack : MonoBehaviour
    {
        [SerializeField] private Image _photoPrefab;
        private readonly List<GameObject> _photos = new List<GameObject>();

        private bool _isSpreaded;
        
        public void CreatePhoto(int pose)
        {
            var photo = Instantiate(_photoPrefab, transform);
            photo.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-10f, 10f));
            photo.sprite = GameManager.Instance.gameData.poseSprites[pose];
            
            _photos.Add(photo.gameObject);
        }

        public Sequence SpreadPhotosSequence()
        {
            if (_isSpreaded)
                return null;

            var seq = DOTween.Sequence();
            foreach (var photo in _photos)
            {
                var randomPosition = new Vector2(Random.Range(0f, -Screen.width / 1.75f), Random.Range(0f, Screen.height / 2f));
                var rect = photo.GetComponent<RectTransform>();
                seq.Join(rect.DOAnchorPos(randomPosition, 1f));
            }
            
            _isSpreaded = true;
            return seq;
        }
    }
}