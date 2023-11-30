using UnityEngine;

namespace Framework
{
    public class ParticleHandler : MonoBehaviour
    {
        ParticleSystem[] _particles;

        public ParticleSystem[] Particles
        {
            get
            {
                if (_particles == null)
                    _particles = GetComponentsInChildren<ParticleSystem>();
                return _particles;
            }
        }

        public void SetEmission(bool enabled)
        {
            for (int i = 0; i < Particles.Length; i++)
            {
                var emission = _particles[i].emission;
                emission.enabled = enabled;
            }
        }

        public void Play()
        {
            for (int i = 0; i < Particles.Length; i++)
            {
                _particles[i].Play();
            }
        }
    }
}