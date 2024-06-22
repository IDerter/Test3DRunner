using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButchersGames
{
    public class SimpleTimer : MonoBehaviour
    {
        public event Action OnEndTime;

        [SerializeField] private float _timeRemaining = 10f; // Задаем начальное время таймера
        public float GetTime => _timeRemaining;

        [SerializeField] private float _startTimer;
        public float GetStartTime => _startTimer;

        [SerializeField] private bool _timerIsRunning = false;
        public bool TimerIsRunning => _timerIsRunning;

        private void Start()
        {
            _startTimer = _timeRemaining;
        }

        public void StartTimer(float duration)
        {
            _timeRemaining = duration;
            _timerIsRunning = true;
        }

        public void ResetTimer(float duration)
        {
            _timerIsRunning = false;
            _timeRemaining = duration;
        }

        void Update()
        {
            if (_timerIsRunning)
            {
                if (_timeRemaining > 0)
                {
                    _timeRemaining -= Time.deltaTime;
                }
                else
                {
                    Debug.Log("Время вышло! " + gameObject.name);
                    OnEndTime?.Invoke();
                    _timerIsRunning = false;
                }
            }
        }
    }
}