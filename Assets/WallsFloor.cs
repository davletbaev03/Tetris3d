using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KKS.Tetris
{
    public enum ColliderType
    {
        WallLeft = 1,
        WallRight = 2,
        Floor = 3,
        Blocks = 4,
    }
    
    [RequireComponent(typeof(Collider))]
public class WallsFloor : MonoBehaviour
    {
        [SerializeField]
        private ColliderType type;
        public ColliderType Type => type;
    }
}
