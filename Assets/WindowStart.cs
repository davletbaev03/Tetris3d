using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KKS.Tetris
{
    public class WindowStart : MonoBehaviour
    {
        [SerializeField] private Button buttonStart = null;
        [SerializeField] private Spawner spawner = null;

        private void Start()
        {
            buttonStart.onClick.AddListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            Debug.LogError("StartClick");
            SetState(false);
            spawner.SpawnNext();
        }

        public void SetState(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
