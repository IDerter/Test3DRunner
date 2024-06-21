using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ButchersGames
{
    public class SwipeDetection : SingletonBase<SwipeDetection>
    {
        public static event UnityAction<float> OnSwipeInput;

        private Vector2 _tapPosition;
        private Vector2 _swipeDelta;

        private float _deadZone = 100f;

        private bool _isSwiping;
        public bool GetIsSwipe => _isSwiping;

        private bool _isMobile;

        private void Start()
        {
            _isMobile = Application.isMobilePlatform;
        }

        private void Update()
        {
            if (!_isMobile)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _isSwiping = true;
                    _tapPosition = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    ResetSwipe();
                }
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        _isSwiping = true;
                        _tapPosition = Input.GetTouch(0).position;
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Canceled ||
                        Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        ResetSwipe();
                    }
                }
            }

            CheckSwipe();
        }

        private void CheckSwipe()
        {
            _swipeDelta = Vector2.zero;

            if (_isSwiping)
            {
                if (!_isMobile && Input.GetMouseButton(0))
                {
                    _swipeDelta = (Vector2)Input.mousePosition - _tapPosition;
                }
                else if (Input.touchCount > 0)
                {
                    _swipeDelta = Input.GetTouch(0).position - _tapPosition;
                }
            }
         //   Debug.Log(_swipeDelta);
            if (_swipeDelta.magnitude > _deadZone)
            {
                if (OnSwipeInput != null)
                {
                    OnSwipeInput?.Invoke(_swipeDelta.x); 
                }

                ResetSwipe();
            }
        }

        private void ResetSwipe()
        {
            _isSwiping = false;

            _tapPosition = Vector2.zero;
            _swipeDelta = Vector2.zero;
        }
    }
}