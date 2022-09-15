using BathroomSelfie.Gameplay.UI;
using BathroomSelfie.UI;
using UnityEngine;

namespace BathroomSelfie.Manager
{
    public sealed class UIManager : Singleton<UIManager>
    {
        public UIObject levelCompletedPanel;
        public UIObject gamePanel;

        [Space] public UIText levelText;
        public UIChat chat;
    }
}