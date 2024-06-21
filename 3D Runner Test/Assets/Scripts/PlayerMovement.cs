using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButchersGames
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _limitValueX;
        private bool _isMobile;
        private bool _swipeDetect = false;

        private void Start()
        {
            _isMobile = Application.isMobilePlatform;
         //   SwipeDetection.OnSwipeInput += OnSwipeInput;
        }

        private void OnDestroy()
        {
        //    SwipeDetection.OnSwipeInput -= OnSwipeInput;
        }

        //private void OnSwipeInput(float xSwipe)
        //{
        //    float halfScreen = Screen.width / 2;
        //    float xPos = (xSwipe - halfScreen) / halfScreen;
        //    float finalXPos = Mathf.Clamp(xPos * _limitValueX, -_limitValueX, _limitValueX);
        //    Debug.Log("")
        //}

        private void Update()
        {
            

            if (_isMobile)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    // Проверяем фазу касания
                    if (touch.phase == TouchPhase.Moved)
                    {
                        float finalXPos = Mathf.Clamp(_playerTransform.localPosition.x + touch.deltaPosition.x * _limitValueX, -_limitValueX, _limitValueX);
                        // Вычисляем новую целевую позицию на основе касания
                        _playerTransform.localPosition = Vector3.Lerp(_playerTransform.localPosition, new Vector3(finalXPos,
                            _playerTransform.localPosition.y), Time.deltaTime * 5);
                    }
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    MovePlayer();
                }
            }
        }

        private void MovePlayer()
        {
            float halfScreen = Screen.width / 2;
            float xPos = (Input.mousePosition.x - halfScreen) / halfScreen;
            float finalXPos = Mathf.Clamp(xPos * _limitValueX, -_limitValueX, _limitValueX);

            _playerTransform.localPosition = Vector3.Lerp(_playerTransform.localPosition, new Vector3(finalXPos, _playerTransform.localPosition.y), Time.deltaTime * 5);
            
        }
    }
}