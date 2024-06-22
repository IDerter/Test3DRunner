using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ButchersGames
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private float _delayScore = 0.05f;
        [SerializeField] private float _distance = 130f; // Расстояние, на которое объект будет поднят или опущен
        [SerializeField] private float _duration = 1f;

        [SerializeField] private int _currentScore = 0;
        public int CurrentScore => _currentScore;

        [SerializeField] private int _displayedScore = 0; 
        [SerializeField] private TextMeshProUGUI _scoreText;

        [SerializeField] private TextMeshProUGUI _scoreToAddTextPlus;
        [SerializeField] private int _scoreToAddAllPlus;
        [SerializeField] private Image _greenDollarImage;

        [SerializeField] private TextMeshProUGUI _scoreToAddTextMinus;
        [SerializeField] private int _scoreToAddAllMinus;
        [SerializeField] private Image _redDollarImage;

        [SerializeField] private SimpleTimer _timerPlus;
        [SerializeField] private SimpleTimer _timerMinus;

        [SerializeField] private Image _sliderImage;
        [SerializeField] private GameSO _gameSO;
        [SerializeField] private TextMeshProUGUI _textAboveSlider;

        private void Update()
        {
            if (_sliderImage.fillAmount != _displayedScore / SkinsChanger.Instance.GetCostAllDollars)
                _sliderImage.fillAmount = Mathf.Lerp(_sliderImage.fillAmount, _displayedScore / SkinsChanger.Instance.GetCostAllDollars, Time.deltaTime * 3);
        }

        void Start()
        {
            _displayedScore = PickUpController.Instance.GetScore;
            _currentScore = _displayedScore;

            SetSliderColor();

            _timerPlus.OnEndTime += OnEndTimePlus;
            _timerMinus.OnEndTime += OnEndTimeMinus;

            _scoreText.text =  _displayedScore.ToString();
            PickUpController.Instance.OnUpdateScorePlus += OnUpdateScorePlus;
            PickUpController.Instance.OnUpdateScoreMinus += OnUpdateScoreMinus;
        }

        private void OnDestroy()
        {
            PickUpController.Instance.OnUpdateScorePlus -= OnUpdateScorePlus;
            PickUpController.Instance.OnUpdateScoreMinus -= OnUpdateScoreMinus;
            _timerPlus.OnEndTime -= OnEndTimePlus;
        }

        private void OnEndTimeMinus()
        {
            _scoreToAddAllMinus = 0;

            if (_scoreToAddTextMinus != null)
            {
                _scoreToAddTextMinus.DOColor(new Color(_scoreToAddTextMinus.color.r, _scoreToAddTextMinus.color.g, _scoreToAddTextMinus.color.b, 0), _duration);
                _scoreToAddTextMinus.GetComponent<RectTransform>().DOAnchorPosY(_scoreToAddTextMinus.GetComponent<RectTransform>().anchoredPosition.y - _distance, _duration);

                _redDollarImage.GetComponent<RectTransform>().DOAnchorPosY(_redDollarImage.GetComponent<RectTransform>().anchoredPosition.y - _distance, _duration);
                _redDollarImage.DOColor(new Color(_redDollarImage.color.r, _redDollarImage.color.g, _redDollarImage.color.b, 0), _duration);
            }
        }

        private void OnEndTimePlus()
        {
            _scoreToAddAllPlus = 0;
            
             if (_scoreToAddTextPlus != null)
             {
                _scoreToAddTextPlus.DOColor(new Color(_scoreToAddTextPlus.color.r, _scoreToAddTextPlus.color.g, _scoreToAddTextPlus.color.b, 0), _duration);
                _scoreToAddTextPlus.GetComponent<RectTransform>().DOAnchorPosY(_scoreToAddTextPlus.GetComponent<RectTransform>().anchoredPosition.y + _distance, _duration);

                _greenDollarImage.GetComponent<RectTransform>().DOAnchorPosY(_greenDollarImage.GetComponent<RectTransform>().anchoredPosition.y + _distance, _duration);
                _greenDollarImage.DOColor(new Color(_greenDollarImage.color.r, _greenDollarImage.color.g, _greenDollarImage.color.b, 0), _duration);
             }
        }

        private void OnUpdateScoreMinus(int scoreToAdd)
        {
          
            _timerMinus.StartTimer(_timerMinus.GetStartTime);

            _currentScore += scoreToAdd;
            
            _scoreToAddTextMinus.DOColor(new Color(_scoreToAddTextMinus.color.r, _scoreToAddTextMinus.color.g, _scoreToAddTextMinus.color.b, 1), 1);
            _redDollarImage.DOColor(new Color(_redDollarImage.color.r, _redDollarImage.color.g, _redDollarImage.color.b, 0), 1);

            _scoreToAddAllMinus += scoreToAdd;

            StartCoroutine(UpdateScoreRoutineMinus());
        }

        private void OnUpdateScorePlus(int scoreToAdd)
        {
      
            _timerPlus.StartTimer(_timerPlus.GetStartTime);


            _currentScore += scoreToAdd;

            _scoreToAddTextPlus.DOColor(new Color(_scoreToAddTextPlus.color.r, _scoreToAddTextPlus.color.g, _scoreToAddTextPlus.color.b, 1), 1);
            _greenDollarImage.DOColor(new Color(_greenDollarImage.color.r, _greenDollarImage.color.g, _greenDollarImage.color.b, 0), 1);

            _scoreToAddAllPlus += scoreToAdd;

            // Запускаем корутину для плавного увеличения счета
            StartCoroutine(UpdateScoreRoutinePlus());
        }

        IEnumerator UpdateScoreRoutineMinus()
        {
            while (_displayedScore > _currentScore)
            {
                _displayedScore--;
                _scoreText.text = _displayedScore.ToString();
                _scoreToAddTextMinus.text = _scoreToAddAllMinus.ToString();

                _sliderImage.fillAmount = Mathf.Lerp(_sliderImage.fillAmount, _displayedScore / SkinsChanger.Instance.GetCostAllDollars, Time.deltaTime * 3);
                SetSliderColor();

                yield return new WaitForSeconds(_delayScore); // Задержка для плавности анимации
            }
        }

        IEnumerator UpdateScoreRoutinePlus()
        {
            while (_displayedScore < _currentScore)
            {
                _displayedScore++;
                _scoreText.text = _displayedScore.ToString();
                _scoreToAddTextPlus.text = "+" + _scoreToAddAllPlus.ToString();

                _sliderImage.fillAmount = Mathf.Lerp(_sliderImage.fillAmount, _displayedScore / SkinsChanger.Instance.GetCostAllDollars, Time.deltaTime * 3);
                SetSliderColor();

                yield return new WaitForSeconds(_delayScore); // Задержка для плавности анимации
            }
        }

        private void SetSliderColor()
        {
            if (_sliderImage.fillAmount < 0.5)
            {
                _sliderImage.color = Color.red;
                SkinsChanger.Instance.TypeDollarOwner = TypeDollarsOwner.poor;
                SkinsChanger.Instance.ChangeSkin();
                _textAboveSlider.text = _gameSO.PoorString;
            }
            if (_sliderImage.fillAmount >= 0.5 && _sliderImage.fillAmount < 0.85)
            {
                SkinsChanger.Instance.TypeDollarOwner = TypeDollarsOwner.medium;
                SkinsChanger.Instance.ChangeSkin();
                _sliderImage.color = Color.yellow;
                _textAboveSlider.text = _gameSO.MediumString;
            }
            if (_sliderImage.fillAmount > 0.85)
            {
                SkinsChanger.Instance.TypeDollarOwner = TypeDollarsOwner.rich;
                SkinsChanger.Instance.ChangeSkin();
                _sliderImage.color = Color.green;
                _textAboveSlider.text = _gameSO.RichString;
            }
            _textAboveSlider.color = _sliderImage.color;
        }
    }
}