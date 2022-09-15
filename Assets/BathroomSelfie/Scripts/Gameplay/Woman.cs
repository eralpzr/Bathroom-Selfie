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

        
    }
}