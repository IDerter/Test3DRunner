using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace ButchersGames
{
    public class BottleExplosion : MonoBehaviour
    {
        [SerializeField] private float punchAmount = 1.05f; // Величина увеличения
        [SerializeField] private float duration = 0.5f; // Продолжительность анимации
        [SerializeField] private int vibrato = 1; // Количество вибраций
        [SerializeField] private float elasticity = 1f; // Эластичность
        [SerializeField] private ParticleSystem _bottleParticleSystem;
        [SerializeField] private Transform _parentSpawn;

        private float _rotationParticlesX = -90f;
        private float _timeToDieParticles = 2f;


        void OnDestroy()
        {
            ParticlePlay();
        }

        private void ParticlePlay()
        {
            ParticleSystem explosionEffect = Instantiate(_bottleParticleSystem, _parentSpawn.position, transform.rotation, _parentSpawn);

            var particleRenderer = explosionEffect.GetComponent<ParticleSystemRenderer>();
            // Получаем материал системы частиц
            Material particleMaterial = particleRenderer.material;

            // Установка начальной прозрачности материала в 0
            Color initialColor = particleMaterial.GetColor("_TintColor");
            particleMaterial.SetColor("_TintColor", new Color(initialColor.r, initialColor.g, initialColor.b, 0));

            particleMaterial.DOColor(new Color(initialColor.r, initialColor.g, initialColor.b, 1), "_TintColor", 1).SetEase(Ease.Linear);

            var rotation = explosionEffect.transform.rotation.eulerAngles;
            rotation.x = _rotationParticlesX; // Задайте нужное значение угла в градусах
            explosionEffect.transform.rotation = Quaternion.Euler(rotation);

            explosionEffect.Play();
            Destroy(explosionEffect.gameObject, _timeToDieParticles);
        }
    }
}