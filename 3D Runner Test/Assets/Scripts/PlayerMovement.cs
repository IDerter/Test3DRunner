using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButchersGames
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _limitValueX;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MovePlayer();
            }
        }

        private void MovePlayer()
        {
            float halfScreen = Screen.width / 2;
            float xPos = (Input.mousePosition.x - halfScreen) / halfScreen;
            float finalXPos = Mathf.Clamp(xPos * _limitValueX, -_limitValueX, _limitValueX);

            _playerTransform.localPosition = new Vector3(finalXPos, _playerTransform.localPosition.y, 0);   
        }
    }
}