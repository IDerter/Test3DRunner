using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButchersGames
{
    public class RotatePickUp : MonoBehaviour
    {
        private float _time = 12f;

        private void Start()
        {
            transform.DOLocalRotate(new Vector3(0, 360, 0), _time, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
    }
}