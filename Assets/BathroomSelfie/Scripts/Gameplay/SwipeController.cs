using System;
using BathroomSelfie.Enums;
using BathroomSelfie.Manager;
using UnityEngine;

namespace BathroomSelfie.Gameplay
{
    public sealed class SwipeController : MonoBehaviour
    {
        private Vector3 _firstMousePosition;
        private Vector2 _swipe;

        private void Update()
        {
            if (GameManager.Instance.GameState != GameState.Playing)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                if (Input.touchCount > 1)
                    return;

                _firstMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _swipe = Input.mousePosition - _firstMousePosition;
                _swipe.Normalize();

                ArrowDirection swipeDirection = ArrowDirection.Up;
                if(_swipe.y > 0 && _swipe.x > -0.5f && _swipe.x < 0.5f) // Up
                {
                    swipeDirection = ArrowDirection.Up;
                }
                else if(_swipe.y < 0 && _swipe.x > -0.5f && _swipe.x < 0.5f) // Down
                {
                    swipeDirection = ArrowDirection.Down;
                }
                else if(_swipe.x < 0 && _swipe.y > -0.5f && _swipe.y < 0.5f) // Left
                {
                    swipeDirection = ArrowDirection.Left;
                }
                else if(_swipe.x > 0 && _swipe.y > -0.5f && _swipe.y < 0.5f) // Right
                {
                    swipeDirection = ArrowDirection.Right;
                }

                if (UIManager.Instance.arrowPanel.SelectedArrow && UIManager.Instance.arrowPanel.SelectedArrow.Direction == swipeDirection)
                {
                    UIManager.Instance.arrowPanel.HighlightBox(Color.green);
                    UIManager.Instance.flash.Flash(.15f);
                    
                    var arrow = UIManager.Instance.arrowPanel.SelectedArrow;
                    arrow.FadeOutDestroy();

                    var pose = GetPoseFromDirection(swipeDirection);
                    UIManager.Instance.photoStack.CreatePhoto(pose);
                    GameManager.Instance.woman.DoPose(pose);
                }
                else
                {
                    UIManager.Instance.arrowPanel.HighlightBox(Color.red);
                }
            }
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