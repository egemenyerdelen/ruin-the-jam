using System;
using Helpers;
using Input;
using UnityEngine;

namespace Core
{
    public class GameManager : Singleton<GameManager>
    {
        public static event Action<GameState> OnGameStateChanged;
        public static GameState ActiveGameState
        {
            get => _activeGameState;
            set
            {
                if (!_activeGameState.Equals(value))
                {
                    _activeGameState = value;
                    OnGameStateChanged?.Invoke(_activeGameState);
                }
            }
        }

        private static GameState _activeGameState = GameState.Playing;

        private void Start()
        {
            if (_activeGameState == GameState.Paused)
            {
                PauseGame();
            }

            if (_activeGameState == GameState.Playing)
            {
                ResumeGame();
            }
        }

        public static void StartGame()
        {
            ActiveGameState = GameState.Playing;
            
            InputSwitcher.Instance.SwitchController(ControllerType.Player);
        }

        public static void ResumeGame()
        {
            ActiveGameState = GameState.Playing;

            Time.timeScale = 1;
        }
        
        public static void PauseGame()
        {
            Time.timeScale = 0;
            
            ActiveGameState = GameState.Paused;
        }

        public static void EndLevel()
        {
            PauseGame();
            
            
            // Will add upgrade menu and open it in here
        }
        
    }

    public enum GameState
    {
        Playing,
        Paused,
        Loading
    }
}