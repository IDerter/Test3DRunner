using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButchersGames
{
    public class RotateFlagCheckpoint : MonoBehaviour
    {
        private float _time = 0.5f;
        [SerializeField] private Transform _flagLeft;
        [SerializeField] private Transform _flagRight;

        [SerializeField] private bool _isActivate = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_isActivate)
            {
                RotateFlags();
                Debug.Log("Player");
                AudioManager.Instance.PlaySound("CheckPoint");
                //звук;
            }
        }


        public void RotateFlags()
        {
            if (_flagLeft != null)
                _flagLeft.DOLocalRotate(new Vector3(0, 0, -90), _time, RotateMode.Fast).SetRelative(true).SetEase(Ease.Linear);

            if (_flagRight != null)
                _flagRight.DOLocalRotate(new Vector3(0, 0, -90), _time, RotateMode.Fast).SetRelative(true).SetEase(Ease.Linear);

            _isActivate = true;
        }
    }
}