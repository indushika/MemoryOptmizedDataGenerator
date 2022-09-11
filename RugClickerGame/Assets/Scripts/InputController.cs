using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace InputCombinationSystem
{
    public class InputController : MonoBehaviour
    {
        public static event Action OnLeftClick; 
        public static event Action OnRightClick; 
        public static event Action OnMiddleClick; 
        public static event Action OnRightLeftComboClick; 
        public static event Action OnDoubleLeftClick; 
        public static event Action OnDoubleRightClick; 
        public static event Action OnDoubleMiddleClick;
        public static event Action OnMouseScrollUp;
        public static event Action OnMouseScrollDown;
        public static event Action OnLeftHoldDown;
        public static event Action OnRightHoldDown;
        public static event Action OnRightLeftHoldDown;
        public static event Action OnLeftUp;
        public static event Action OnRightUp;
        public static event Action OnMiddleUp;
        public static event Action OnRightLeftUp;


        public float _interval;
        public float _holdDownInterval; 

        private float _timePassed;
        private float _leftHoldTimer; 
        private float _rightHoldTimer; 
        private int _inputCount;
        private bool _leftClick, _rightClick, _middleClick, _mouseScrollUp, _mouseScrollDown, _leftHoldDown, _rightHoldDown, _leftUp, _rightUp, _middleUp;

        public InputController(float interval)
        {
            _interval = interval;
        }

        public void Tick(float deltaTime)
        {
            _timePassed += deltaTime;

        }

        public void LeftClick()
        {
            _inputCount++;
            _leftClick = true;
        }
        public void RightClick()
        {
            _inputCount++;
            _rightClick = true;
        }
        public void MiddleClick()
        {
            _inputCount++;
            _middleClick = true;
        } 
        public void MouseScrollUp()
        {
            _mouseScrollUp = true; 
        }
        public void MouseScrollDown()
        {
            _mouseScrollDown = true; 
        }
        public void LeftHold()
        {
            _leftHoldDown = true; 
        }
        public void RightHold()
        {
            _rightHoldDown = true; 
        }
        public void LeftUp()
        {
            _leftUp = true; 
        }
        public void RightUp()
        {
            _rightUp = true; 
        }
        public void MiddleUp()
        {
            _middleUp = true; 
        }

        private void Update()
        {
            Tick(Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                LeftClick();
            }

            if (Input.GetMouseButtonDown(1))
            {
                RightClick();
            }

            if (Input.GetMouseButtonDown(2))
            {
                MiddleClick();
            }

            if (Input.GetMouseButton(0))
            {
                _leftHoldTimer += Time.deltaTime;
                if (_leftHoldTimer>=_holdDownInterval)
                {
                    LeftHold(); 
                }
            }
            if (Input.GetMouseButton(1))
            {
                _rightHoldTimer += Time.deltaTime;
                if (_rightHoldTimer>=_holdDownInterval)
                {
                    RightHold(); 
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                _leftHoldTimer = 0;
                LeftUp(); 
            }
            if (Input.GetMouseButtonUp(1))
            {
                _rightHoldTimer = 0;
                RightUp(); 
            }



            if (Input.mouseScrollDelta.y>0f)
            {
                MouseScrollUp(); 
            }
            if (Input.mouseScrollDelta.y < 0f)
            {
                MouseScrollDown(); 
            }


            CheckInputAction(); 
        }



        private bool CheckIntervalEnd()
        {
            bool intervalEnd;
            if (_timePassed <= _interval)
            {
                intervalEnd = false;
            }
            else
            {
                intervalEnd = true;
                _timePassed = 0;
            }

            return intervalEnd;
        }

        public void CheckInputAction()
        {
            if (CheckIntervalEnd())
            {
                if (_leftHoldDown)
                {
                    OnLeftHoldDown?.Invoke();
                    if (_rightHoldDown)
                    {
                        OnRightLeftHoldDown?.Invoke();
                    }
                }
                if (_rightHoldDown)
                {
                    OnRightHoldDown?.Invoke();
                }
                if (_leftUp)
                {
                    OnLeftUp?.Invoke();
                    OnRightLeftUp?.Invoke();
                }
                if (_rightUp)
                {
                    OnRightUp?.Invoke();
                    OnRightLeftUp?.Invoke();
                }
                if (_middleUp)
                {
                    OnMiddleUp?.Invoke(); 
                }

                if (_mouseScrollUp)
                {
                    OnMouseScrollUp?.Invoke(); 
                }
                else if (_mouseScrollDown)
                {
                    OnMouseScrollDown?.Invoke(); 
                }
                if (_inputCount == 1)
                {
                    if (_leftClick)
                    {
                        OnLeftClick?.Invoke(); 
                    }
                    else if (_rightClick)
                    {
                        OnRightClick?.Invoke(); 
                    }
                    else if (_middleClick)
                    {
                        OnMiddleClick?.Invoke();
                    }
                }
                else if (_inputCount == 2)
                {
                    if (_leftClick)
                    {
                        if (_rightClick)
                        {
                            OnRightLeftComboClick?.Invoke(); 
                        }
                        else if (_leftClick)
                        {
                            OnDoubleLeftClick?.Invoke();
                        }
                    }
                    else if (_rightClick)
                    {
                        if (_leftClick)
                        {
                            OnRightLeftComboClick?.Invoke(); 
                        }
                        else if (_rightClick)
                        {
                            OnDoubleRightClick?.Invoke();
                        }
                    }

                }



                _leftClick = false;
                _rightClick = false;
                _middleClick = false;
                _mouseScrollUp = false;
                _mouseScrollDown = false;
                _leftHoldDown = false;
                _rightHoldDown = false;
                _leftUp = false;
                _rightUp = false;
                _middleUp = false; 

                _inputCount = 0;
            }

        }

    }

}
