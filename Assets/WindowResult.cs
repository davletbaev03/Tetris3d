using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KKS.Tetris
{
    public class WindowResult : MonoBehaviour
    {
        [SerializeField] private Button buttonRestart = null;
        private GameController gameController = null;

        private void Start()
        {
            buttonRestart.onClick.AddListener(OnRestartClicked);
            SetState(false);
        }

        private void OnRestartClicked()
        {
            Debug.LogError("RestartClick");
            gameController.Restart();
        }

        public void Init(GameController gameController)
        {
            this.gameController = gameController;
        }

        public void SetState(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}