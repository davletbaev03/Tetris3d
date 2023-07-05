using KKS.Tetris;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KKS.Tetris
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private WindowResult windowResult = null;
        [SerializeField] private Spawner spawner = null;
        [SerializeField] private RowControl rowControl = null;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            spawner.OnGameEnd += ShowResult;
            windowResult.Init(this);
            spawner.OnBlockSpawn += rowControl.RowsCheck;
        }

        public void Restart()
        {
            spawner.OnRestart();
            windowResult.SetState(false);
        }

        public void ShowResult()
        {
            windowResult.SetState(true);
        }

    }
}