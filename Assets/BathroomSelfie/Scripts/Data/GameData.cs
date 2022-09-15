using BathroomSelfie.Enums;
using UnityEngine;

namespace BathroomSelfie.Data
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Bathroom Selfie/Game Data", order = 0)]
    public class GameData : ScriptableObject
    {
        public Sprite[] poseSprites;
        
        [Space]
        public float arrowMoveDuration;
        public float arrowWaitDuration;
        public ArrowDirection[] arrowDirections;
    }
}