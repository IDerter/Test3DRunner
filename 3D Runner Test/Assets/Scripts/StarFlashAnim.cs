using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace ButchersGames
{
    public class StarFlashAnim : MonoBehaviour
    {
        [SerializeField] private float punchAmount = 1.05f; // Величина увеличения
        [SerializeField] private float duration = 0.5f; // Продолжительность анимации
        [SerializeField] private int vibrato = 1; // Количество вибраций
        [SerializeField] private float elasticity = 1f; // Эластичность
        private Image _imageStar;
        [SerializeField] private RectTransform _splash;
        [SerializeField] private Image _splashImage;

        private void Start()
        {
            _imageStar = GetComponent<Image>();
        }

        public void StarAnim()
        {
            _imageStar.DOColor(new Color(_imageStar.color.r, _imageStar.color.g, _imageStar.color.b, 1), duration / 2);

            transform.DOPunchScale(new Vector3(punchAmount, punchAmount, punchAmount), duration, vibrato, elasticity).SetUpdate(true).
                OnComplete(() => transform.DOScale(new Vector3(1, 1, 1), duration));

            transform.DOLocalRotate(new Vector3(0, 0, 45), duration, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).OnComplete(
                () => _imageStar.DOColor(new Color(_imageStar.color.r, _imageStar.color.g, _imageStar.color.b, 0), duration / 2));

            _splashImage.DOColor(new Color(_splashImage.color.r, _splashImage.color.g, _splashImage.color.b, 0.5f), duration / 2);


            _splash.DOLocalRotate(new Vector3(0, 0, 45), duration, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).OnComplete(
                () => _splashImage.DOColor(new Color(_splashImage.color.r, _splashImage.color.g, _splashImage.color.b, 0), duration / 2));
        }
    }
}