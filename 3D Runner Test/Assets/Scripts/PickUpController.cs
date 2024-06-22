using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ButchersGames
{
    public class PickUpController : SingletonBase<PickUpController>
    {
        public event Action OnFinishGame;
        public event Action OnLoseGame;
        public event Action<int> OnUpdateScorePlus;
        public event Action<int> OnUpdateScoreMinus;

        [SerializeField] private int _score = 40; // Текущий счет игрока

        private int _startValueScore = 40;
        private int _scoreToAdd = 2;
        public int ScoreToAdd => _scoreToAdd;
        private int _scoreToDecrease = -20;
        public int GetScore => _score;

        protected override void Awake()
        {
            base.Awake();
            _score = _startValueScore;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Dollar"))
            {
                _score += _scoreToAdd;
                OnUpdateScorePlus.Invoke(_scoreToAdd);
                AudioManager.Instance.PlaySound("collect_coin");

                Destroy(other.gameObject);
            }

            else if (other.gameObject.CompareTag("Bottle") || other.gameObject.CompareTag("BadDoor"))
            {
                _score += _scoreToDecrease;
                OnUpdateScoreMinus.Invoke(_scoreToDecrease);
                AudioManager.Instance.PlaySound("Aou");

                if (_score <= 0)
                {
                    _score = 0;
                    OnLoseGame.Invoke();
                }
                Destroy(other.gameObject);
            }

            else if (other.gameObject.CompareTag("GoodDoor"))
            {
                _score += Math.Abs(_scoreToDecrease);
                OnUpdateScorePlus.Invoke(Math.Abs(_scoreToDecrease));

                AudioManager.Instance.PlaySound("collect_coin");

                Destroy(other.gameObject);
            }

            if (other.CompareTag("Final"))
            {
                OnFinishGame?.Invoke();
            }
        }
    }
}