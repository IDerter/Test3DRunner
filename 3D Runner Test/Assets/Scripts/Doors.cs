using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButchersGames
{
    public class Doors : MonoBehaviour
    {
        public event Action OnPlayerAddMultiplier;

        [SerializeField] private Transform _doorLeft;
        [SerializeField] private Transform _doorRight;
        [SerializeField] private float _time = 0.25f;

        private const string _audioOpenDoor = "OpenDoor";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OpenDoors();
                AudioManager.Instance.PlaySound(_audioOpenDoor);
                GameManager.Instance.XMultiplier += 1;
            }
        }

        public void OpenDoors()
        {
            _doorLeft.DOLocalRotate(new Vector3(0, -90, 0), _time, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
            _doorRight.DOLocalRotate(new Vector3(0, 90, 0), _time, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
            Destroy(gameObject);
        }
    }
}