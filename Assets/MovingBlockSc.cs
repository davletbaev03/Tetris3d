using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KKS.Tetris
{
    [RequireComponent(typeof(Collider))]
    public class MovingBlockSc : MonoBehaviour
    {

        private float fallSpeed = 1;
        private float timeToFall = 0;
        private bool canMoveLeft = true;
        private bool canMoveRight = true;
        private bool canMoveDown = true;
        private bool canRotate = true;
        public bool CanControl
        {
            get;
            private set;
        }= true;

        public System.Action OnDeactivated
        {
            get;
            set;
        }

        private void Update()
        {
            if (!CanControl)
                return;

            CheckSideKey(KeyCode.LeftArrow);
            CheckSideKey(KeyCode.RightArrow);
            CheckRotateKey(KeyCode.UpArrow);
            CheckDownKey(KeyCode.DownArrow);
            // Проверяем, можно ли переместить объект вниз
            if (Time.time - timeToFall >= 1 / fallSpeed)
            {
                timeToFall = Time.time;

                if (canMoveDown)
                {
                    transform.position += Vector3.down;
                }
            }
        }

        public bool CheckMoveDown()
        {
            return canMoveDown;
        }

        private bool CheckDownKey(KeyCode key)
        {
            bool result = false;
            if (Input.GetKeyDown(key))
            {
                fallSpeed = 10;
                result = true;
            }
            else if (Input.GetKeyUp(key))
            {
                fallSpeed = 1;
            }

            return result;
        }
        private bool CheckSideKey(KeyCode key)
        {
            bool result = false;
            if (Input.GetKeyDown(key))
            {
                if (canMoveLeft)
                {
                    transform.position += key == KeyCode.LeftArrow ? Vector3.left : Vector3.zero;
                }
                if (canMoveRight)
                {
                    transform.position += key == KeyCode.RightArrow ? Vector3.right : Vector3.zero;
                }
            }
            return result;
        }

        private bool CheckRotateKey(KeyCode key)
        {
            bool result = false;
            if (Input.GetKeyDown(key))
            {
                if (canRotate)
                {
                    transform.Rotate(0, 0, -90);
                }
            }
            return result;
        }
        

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetInstanceID() == this.gameObject.GetInstanceID())
            {
                return;
            }

            WallsFloor col = other.gameObject.GetComponent<WallsFloor>();
            if (col != null)
            {
                switch(col.Type)
                {
                    case ColliderType.WallLeft:
                        canMoveLeft = false;
                        break;

                    case ColliderType.WallRight:
                        canMoveRight = false;
                        break;

                    case ColliderType.Floor:
                        CanControl = false;
                        canMoveDown = false;
                        OnDeactivated?.Invoke();
                        break;

                    case ColliderType.Blocks:
                        MovingBlockSc component = other.gameObject.GetComponent<MovingBlockSc>();
                        if (component == null)
                            break;

                        canMoveDown = transform.position.y <= component.transform.position.y
                        || Math.Abs(transform.position.x - component.transform.position.x) >= 0.01f;

                        canMoveLeft &= Math.Abs(transform.position.y - component.transform.position.y) >= 0.01f
                            || transform.position.x <= component.transform.position.x;

                        canMoveRight &= Math.Abs(transform.position.y - component.transform.position.y) >= 0.01f
                            || transform.position.x >= component.transform.position.x;

                        if (!component.CanControl && !canMoveDown)
                        {
                            CanControl = false;
                            OnDeactivated?.Invoke();
                        }

                        break;

                } 
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject == this.gameObject)
            {
                return;
            }

            WallsFloor col = other.GetComponent<WallsFloor>();
            if (col != null)
            {
                switch (col.Type)
                {
                    case ColliderType.WallLeft:
                        canMoveLeft = true;
                        break;

                    case ColliderType.WallRight:
                        canMoveRight = true;
                        break;

                    case ColliderType.Floor:
                        canMoveDown = true;
                        break;

                    case ColliderType.Blocks:
                        //TODO
                        canMoveDown = true;
                        break;

                }
            }
        }

        public void DestroySelf()
        {
            if(this != null && gameObject != null)
                Destroy(gameObject);
        }
    }
}
