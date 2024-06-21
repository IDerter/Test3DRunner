using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButchersGames
{
    public class CameraHolder : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;

        private void Update()
        {
            transform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y,
                _playerTransform.position.z);

            transform.eulerAngles = new Vector3(_playerTransform.eulerAngles.x, _playerTransform.eulerAngles.y,
                _playerTransform.eulerAngles.z);
        }
    }
}