using System;
using Core;
using Helpers;
using Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        public GameObject startScreen;

        //public GameObject inGameMenu;
        public GameObject pauseMenu;

        private InputSystem_Actions _inputActions;

        private void Start()
        {
            Cursor.visible = false;
            _inputActions = InputManager.InputSystem;

            _inputActions.UI.Menu.performed += HandleEscButton;
            _inputActions.UI.SpaceBar.performed += HandleSpaceBar;
        }

        private void OnDisable()
        {
            _inputActions.UI.Menu.performed -= HandleEscButton;
            _inputActions.UI.SpaceBar.performed -= HandleSpaceBar;
        }

        public void StartGame()
        {
            // inGameMenu.SetActive(true);
            startScreen.SetActive(false);
            GameManager.StartGame();
        }

        public void ResumeGame()
        {
            Cursor.visible = false;
            
            EventSystem.current.SetSelectedGameObject(null);

            pauseMenu.SetActive(false);
            // inGameMenu.SetActive(true);
            GameManager.ResumeGame();
        }

        public void PauseGame()
        {
            Cursor.visible = true;
            
            // inGameMenu.SetActive(false);
            pauseMenu.SetActive(true);
            GameManager.PauseGame();
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void HandleSpaceBar(InputAction.CallbackContext obj)
        {
            if (startScreen.activeSelf)
            {
                StartGame();
            }
        }

    private void HandleEscButton(InputAction.CallbackContext obj)
        {
            if (startScreen.activeSelf)
            {
                QuitGame();
            }
            
            switch (GameManager.ActiveGameState)
            {
                case GameState.Paused:
                    ResumeGame();
                    break;
                case GameState.Playing:
                    PauseGame();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}