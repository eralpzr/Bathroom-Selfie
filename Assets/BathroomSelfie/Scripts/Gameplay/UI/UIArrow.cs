using System;
using BathroomSelfie.Enums;
using UnityEngine;

namespace BathroomSelfie.Gameplay.UI
{
    public sealed class UIArrow : MonoBehaviour
    {
        private ArrowDirection _direction;

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