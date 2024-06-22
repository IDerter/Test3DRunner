using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ButchersGames
{
    public class MoneyGet : MonoBehaviour
    {
        [SerializeField] private int _displayedScore;
        [SerializeField] private TextMeshProUGUI _textCountDollarsFinal;
        [SerializeField] private GameObject _panelNextLvl;

        [SerializeField] private ResultUI _textResult;
        [SerializeField] private CoinAnimation _coinAnim;
        [SerializeField] private float _delayScore = 0.005f;

        public void MoneyDisplay()
        {
            StartCoroutine(UpdateScoreRoutinePlus());
            _coinAnim.RewardPileOfCoin();
        }

        IEnumerator UpdateScoreRoutinePlus()
        {
            yield return new WaitForSeconds(1.5f);

            while (_displayedScore < _textResult.GetScoreFinal)
            {
                _displayedScore ++;
                _textCountDollarsFinal.text = _displayedScore.ToString();

                yield return new WaitForSeconds(_delayScore); // Задержка для плавности анимации
            }
            _panelNextLvl.SetActive(true);
        }
    }
}