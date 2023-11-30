using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public static class ParticleSystemExtension
{
    public static void ModifyParticles(this ParticleSystem particleSystem, CallbackRef<Particle> callback)
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        int numParticlesAlive = particleSystem.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            callback?.Invoke(ref particles[i]);
        }

        particleSystem.SetParticles(particles, numParticlesAlive);
    }

    public static void ConvergeToTarget(this ParticleSystem particleSystem, Vector3 target, float multiplier)
    {
        particleSystem.ModifyParticles((ref Particle particle) =>
        {
            Vector3 directionToTarget = (target - particle.position - particleSystem.transform.position);
            Vector3 nextDirection = (particle.velocity.normalized + directionToTarget.normalized / 10).normalized;
            particle.velocity = nextDirection * directionToTarget.magnitude * multiplier;
        });
    }

    public static void DirectToTarget(this ParticleSystem particleSystem, Vector3 target, float multiplier)
    {
        particleSystem.ModifyParticles((ref Particle particle) =>
        {
            Vector3 directionToTarget = (target - particle.position - particleSystem.transform.position);
            particle.velocity = directionToTarget.normalized * directionToTarget.magnitude * multiplier;
        });
    }

    public static void DirectToTargetAfterTime(this ParticleSystem particleSystem, Vector3 target, float multiplier, float timeToMove)
    {
        particleSystem.ModifyParticles((ref Particle particle) =>
        {
            if (particle.remainingLifetime <= particle.startLifetime - timeToMove)
            {
                Vector3 directionToTarget = (target - particle.position - particleSystem.transform.position);
                particle.velocity = directionToTarget.normalized * directionToTarget.magnitude * multiplier;
            }
        });
    }
}
