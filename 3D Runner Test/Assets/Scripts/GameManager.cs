using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

namespace ButchersGames
{
    public class GameManager : SingletonBase<GameManager>
    {
        private const string _animWalk = "walk";
        private const string _animDance = "dance";
        private const string _audioFootStep = "FootStep2";
        private const string _audioWinLvl = "WinLvl";

        [SerializeField] private PathFollower _pathFollower;
        [SerializeField] private GameObject _uiLosePanel;
        [SerializeField] private GameObject _uiStartPanel;
        [SerializeField] private Animator _animatorPlayer;
        [SerializeField] private GameObject _uiFinishGameWin;
        [SerializeField] private GameObject _canvasSlider;
        [SerializeField] private ResultUI _resultUI;

        [SerializeField] private float _xMultiplier;
        public float XMultiplier { get { return _xMultiplier; } set { _xMultiplier = value; } }

        private void Start()
        {
            PickUpController.Instance.OnLoseGame += OnLoseGame;
            PlayerMovement.Instance.OnStartGame += OnStartGame;
            PickUpController.Instance.OnFinishGame += OnFinishGame;
        }

        public void OnFinishGame()
        {
            if (Instance.XMultiplier == ((int)SkinsChanger.Instance.TypeDollarOwner + 2))
            {
                _resultUI.CalculateDollarsMultiplier();
                PlayerMovement.Instance.Play = false;
                _pathFollower.IsGame = false;
                _animatorPlayer.SetBool(_animDance, true);
                _uiFinishGameWin.SetActive(true);
                _canvasSlider.SetActive(false);

                AudioManager.Instance.PlaySound(_audioWinLvl);
                AudioManager.Instance.StopSound(_audioFootStep);
            }
        }

        private void OnStartGame()
        {
            PlayerMovement.Instance.Play = true;
            _pathFollower.IsGame = true;
            _uiStartPanel.SetActive(false);
            _animatorPlayer.SetBool(_animWalk, true);
            _canvasSlider.SetActive(true);

            AudioManager.Instance.PlaySound(_audioFootStep);
        }

        private void OnDestroy()
        {
            PickUpController.Instance.OnLoseGame -= OnLoseGame;
            PlayerMovement.Instance.OnStartGame -= OnStartGame;
            PickUpController.Instance.OnFinishGame -= OnFinishGame;
        }

        private void OnLoseGame()
        {
            PlayerMovement.Instance.Play = false;
            _pathFollower.IsGame = false;
            _uiLosePanel.SetActive(true);
            _canvasSlider.SetActive(false);
            PlayerMovement.Instance.InActivePlayer();

            AudioManager.Instance.StopSound(_audioFootStep);
        }
    }
}