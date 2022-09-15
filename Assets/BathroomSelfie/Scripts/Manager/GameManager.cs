using System.Collections;
using BathroomSelfie.Data;
using BathroomSelfie.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BathroomSelfie.Manager
{
    public sealed class GameManager : Singleton<GameManager>
    {
        public Camera mainCamera;
        public GameData gameData;
        
        public GameState GameState { get; set; }

        protected override void Awake()
        {
            base.Awake();
            GameState = GameState.None;
        }
        
        private void Start()
        {
            GameState = GameState.Playing;
            UIManager.Instance.gamePanel.gameObject.SetActive(true);
            UIManager.Instance.chat.gameObject.SetActive(true);
            UIManager.Instance.arrowPanel.gameObject.SetActive(false);
            RefreshLevelText();

            //StartCoroutine(StartPlayingCoroutine());
            var chatSequence = UIManager.Instance.chat.StartChatSequence();
            chatSequence.onComplete = () => StartCoroutine(StartPlayingCoroutine());
        }

        public IEnumerator StartPlayingCoroutine()
        {
            yield return new WaitForSeconds(1.25f);
            
            UIManager.Instance.chat.gameObject.SetActive(false);
            UIManager.Instance.arrowPanel.gameObject.SetActive(true);

            GameState = GameState.Playing;
            foreach (var direction in gameData.arrowDirections)
            {
                UIManager.Instance.arrowPanel.CreateArrow(direction);
                yield return new WaitForSeconds(gameData.arrowWaitDuration);
            }
        }
        
        public IEnumerator CompleteLevelCoroutine(bool failed)
        {
            GameState = GameState.Completed;
            Progression.Level++;
            
            yield return new WaitForSeconds(1f);
            
            UIManager.Instance.gamePanel.gameObject.SetActive(false);
            UIManager.Instance.levelCompletedPanel.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(1.5f);

            SceneManager.LoadScene("Game");
        }
        
        public void RefreshLevelText()
        {
            UIManager.Instance.levelText.Text = $"Level {Progression.Level}";
        }
    }
}