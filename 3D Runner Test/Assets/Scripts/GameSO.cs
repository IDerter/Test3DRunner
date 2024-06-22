using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButchersGames
{
    [CreateAssetMenu()]
    public class GameSO : ScriptableObject
    {
        [SerializeField] private string _poor = "Бедный";
        public string PoorString => _poor;
        [SerializeField] private string _medium = "Состоятельный";
        public string MediumString => _medium;
        [SerializeField] private string _rich = "Богатый";
        public string RichString => _rich;
    }
}