using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ButchersGames
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textGetDollars;
        [SerializeField] private ScoreUI _scoreUI;
        [SerializeField] private int _scoreFinalXMultiplier;
        public int GetScoreFinal => _scoreFinalXMultiplier;


        public void CalculateDollarsMultiplier()
        {
            _scoreFinalXMultiplier = (int) GameManager.Instance.XMultiplier * _scoreUI.CurrentScore;
            _textGetDollars.text = (_scoreFinalXMultiplier).ToString();
        }
    }
}