using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButchersGames
{
    public enum TypeDollarsOwner
    {
        poor,
        medium,
        rich
    }

    public class SkinsChanger : SingletonBase<SkinsChanger>
    {
        private const string _changeSkin = "ChangeSkin";

        [SerializeField] private Transform _moneyContainer;
        [SerializeField] private float _allDollarsCount;
        [SerializeField] private float _costAllDollars;
        public float GetCostAllDollars => _costAllDollars;
        [SerializeField] private GameObject _poorSkin;
        [SerializeField] private GameObject _mediumSkin;
        [SerializeField] private GameObject _richSkin;
        [SerializeField] private TypeDollarsOwner _type;
        private int _costDollar = 2;
        public TypeDollarsOwner TypeDollarOwner { get { return _type; } set { _type = value; } }

        protected override void Awake()
        {
            base.Awake();

            _allDollarsCount = _moneyContainer.childCount;
            _costAllDollars = _allDollarsCount * _costDollar;
        }

        public void ChangeSkin()
        {
            if (_type == TypeDollarsOwner.poor)
            {
                _poorSkin.SetActive(true);
                _mediumSkin.SetActive(false);
            }
            if (_type == TypeDollarsOwner.medium)
            {
                _poorSkin.SetActive(false);
                _mediumSkin.SetActive(true);
                _richSkin.SetActive(false);
            }
            if (_type == TypeDollarsOwner.rich)
            {
                _richSkin.SetActive(true);
                _mediumSkin.SetActive(false);
            }
        }
    }
}