using UnityEngine;

namespace ButchersGames
{
    public class DollarExplosion : MonoBehaviour
    {
        [SerializeField] private ParticleSystem dollarParticleSystem; 
        [SerializeField] private StarFlashAnim _starFlash;
        [SerializeField] private Transform _parentSpawn;

        private float _timeToDieParticles = 5f;

        void OnDestroy()
        {
            // Создаем экземпляр Particle System при уничтожении объекта
            ParticleSystem explosionEffect = Instantiate(dollarParticleSystem, transform.position, transform.rotation);
            explosionEffect.transform.position += new Vector3(0, dollarParticleSystem.transform.position.y, 0);
            _starFlash.StarAnim();

            explosionEffect.Play();
            Destroy(explosionEffect.gameObject, _timeToDieParticles);
        }
    }
}