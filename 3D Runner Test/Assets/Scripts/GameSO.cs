using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButchersGames
{
    [CreateAssetMenu()]
    public class GameSO : ScriptableObject
    {
        [SerializeField] private string _poor = "������";
        public string PoorString => _poor;
        [SerializeField] private string _medium = "�������������";
        public string MediumString => _medium;
        [SerializeField] private string _rich = "�������";
        public string RichString => _rich;
    }
}