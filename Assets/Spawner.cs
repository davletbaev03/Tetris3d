using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KKS.Tetris
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        public GameObject[] tetrominos;
        private bool canSpawn = true;

        public System.Action OnRestart
        {
            get;
            set;
        }

        public System.Action OnGameEnd
        {
            get;
            set;
        }

        public System.Action<MovingBlockSc> OnBlockSpawn
        {
            get;
            set;
        }

        public void Start()
        {
            OnRestart += Restart;
        }

        public void SpawnNext()
        {
            if (!canSpawn)
                return;

            int index = Random.Range(0, tetrominos.Length - 1);
            
            MovingBlockSc block = Instantiate<GameObject>
                (tetrominos[index], transform.position, Quaternion.identity)
                .GetComponent<MovingBlockSc>();

            if (OnBlockSpawn != null)
                OnBlockSpawn.Invoke(block);

            OnRestart += block.DestroySelf;

            StartCoroutine(Coroutine(block));
        }

        IEnumerator Coroutine(MovingBlockSc block)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            if (block != null && block.CanControl)
                block.OnDeactivated += SpawnNext;
            else
            {
                canSpawn = false;

                if (OnGameEnd != null)
                    OnGameEnd.Invoke();

                Debug.LogError("Konec igri");
            }
        }

        public void Restart()
        {
            canSpawn = true;
            SpawnNext();
        }

    }
}