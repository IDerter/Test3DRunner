using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

namespace ButchersGames
{
    public class CoinAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _pileOfCoinParent;
        [SerializeField] private Transform _moveToObject;

        [SerializeField] private Vector3[] _initialPos;
        [SerializeField] private Quaternion[] _intitialRotation;

        private void Reset()
        {
            for (int i = 0; i < +_pileOfCoinParent.transform.childCount; i++)
            {
                _pileOfCoinParent.transform.GetChild(i).position = _initialPos[i];
                _pileOfCoinParent.transform.GetChild(i).rotation = _intitialRotation[i];
            }
        }

        [Button]
        public void RewardPileOfCoin()
        {
            Reset();

            var delay = 0f;
            _pileOfCoinParent.SetActive(true);

            for (int i = 0; i < _pileOfCoinParent.transform.childCount; i++)
            {
                _pileOfCoinParent.transform.GetChild(i).gameObject.SetActive(true);

                _pileOfCoinParent.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

                _pileOfCoinParent.transform.GetChild(i).GetComponent<RectTransform>().DOMove(_moveToObject.position, 0.8f).SetDelay(delay + 0.5f).SetEase(Ease.InBack);

                _pileOfCoinParent.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f).SetEase(Ease.Flash);

                _pileOfCoinParent.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.8f).SetEase(Ease.OutBack);

                delay += 0.1f;
            }
        }
    }
}