using System;
using BathroomSelfie.Enums;
using UnityEngine;

namespace BathroomSelfie.Gameplay
{
    public sealed class Woman : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void DoPose(int pose)
        {
            if (pose < 0 || pose > 3)
                return;
            
            _animator.SetTrigger($"Pose_{pose}");
        }

        public static int GetPoseFromDirection(ArrowDirection arrowDirection)
        {
            switch (arrowDirection)
            {
                case ArrowDirection.Up:
                    return 0;

                case ArrowDirection.Right:
                    return 3;

                case ArrowDirection.Down:
                    return 1;

                case ArrowDirection.Left:
                    return 2;

                default:
                    throw new ArgumentOutOfRangeException(nameof(arrowDirection), arrowDirection, null);
            }
        }
    }
}