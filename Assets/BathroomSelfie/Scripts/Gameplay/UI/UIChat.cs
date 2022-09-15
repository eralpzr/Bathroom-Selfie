using BathroomSelfie.UI;
using DG.Tweening;
using UnityEngine;

namespace BathroomSelfie.Gameplay.UI
{
    public sealed class UIChat : MonoBehaviour
    {
        [SerializeField] private float _scaleTime;
        [SerializeField] private float _waitTime;
        [SerializeField] private UIObject[] _chatBubbles;

        private Sequence _sequence;
        
        public Sequence StartChatSequence()
        {
            _sequence?.Rewind();
            _sequence?.Kill();
            
            _sequence = DOTween.Sequence();
            for (int i = 0; i < _chatBubbles.Length; i++)
            {
                _chatBubbles[i].transform.localScale = Vector3.zero;
                _sequence.Append(_chatBubbles[i].transform.DOScale(Vector3.one, _scaleTime).SetDelay(_waitTime));
            }

            return _sequence;
        }
    }
}