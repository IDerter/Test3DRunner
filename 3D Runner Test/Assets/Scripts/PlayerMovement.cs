using PathCreation.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButchersGames
{
    public class PlayerMovement : SingletonBase<PlayerMovement>
    {
        public event Action OnStartGame;

        [SerializeField] private Transform _playerTransform;

        [SerializeField] private float _limitValueX;
        [SerializeField] private float _speedMoveX = 4f;
        [SerializeField] private bool _isPlay = false;
        public bool Play { get { return _isPlay; } set { _isPlay = value; } }

        private float _mistakeOffset = 10f;
        private bool _isMobile;
        private bool _isMoving = false;

        private void Start()
        {
            _isMobile = Application.isMobilePlatform;
        }

        public void InActivePlayer()
        {
            _playerTransform.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_isPlay)
            {
                if (_isMobile)
                {
                    if (Input.touchCount > 0)
                    {
                        _isMoving = true;
                        Touch touch = Input.GetTouch(0);
                        // Проверяем фазу касания
                        if (touch.phase == TouchPhase.Moved)
                        {
                            if (Math.Abs(touch.deltaPosition.x) > _mistakeOffset)
                            {
                                float finalXPos = Mathf.Clamp(_playerTransform.localPosition.x + touch.deltaPosition.x * _limitValueX, -_limitValueX, _limitValueX);
                                // Вычисляем новую целевую позицию на основе касания
                                _playerTransform.localPosition = Vector3.Lerp(_playerTransform.localPosition, new Vector3(finalXPos,
                                    _playerTransform.localPosition.y), Time.deltaTime * _speedMoveX);
                            }
                        }
                    }
                }
                else
                {
                    if (Input.GetMouseButton(0))
                    {
                        _isMoving = true;
                        MovePlayer();
                    }
                }
            }

            if (_isMobile && !_isMoving)
            {
                if (Input.touchCount > 0)
                {
                    OnStartGame.Invoke();
                }
            }
            else
            {
                if (Input.GetMouseButton(0) && !_isMoving)
                {
                    OnStartGame.Invoke();
                }
            }
        }

        private void MovePlayer()
        {
            float halfScreen = Screen.width / 2;
            float xPos = (Input.mousePosition.x - halfScreen) / halfScreen;
            float finalXPos = Mathf.Clamp(xPos * _limitValueX, -_limitValueX, _limitValueX);

            _playerTransform.localPosition = Vector3.Lerp(_playerTransform.localPosition, 
                new Vector3(finalXPos, _playerTransform.localPosition.y), Time.deltaTime * _speedMoveX);
        }
    }
}