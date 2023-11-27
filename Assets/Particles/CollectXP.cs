using System.Collections.Generic;
using UnityEngine;

public class CollectXP : MonoBehaviour
{
    ParticleSystem ps;
    List<ParticleSystem.Particle> particles = new();
    public int particlesCollected = 0;
    private GameObject player;
    public int xpGiven = 1000;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        player = GameObject.FindWithTag("Player");

        // Set the maxParticles property
        var mainModule = ps.main;
        mainModule.maxParticles = 1000;
    }

    private void OnParticleTrigger()     
    {
        int triggeredParticles = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
        
        for (int i = 0; i < triggeredParticles; i++)
        {
            ParticleSystem.Particle p = particles[i];
            p.remainingLifetime = 0;
            player.GetComponent<PlayerAttributes>().xp = player.GetComponent<PlayerAttributes>().xp + 10;
            particlesCollected++;
            particles[i] = p;
            var mainModule = ps.main;
            mainModule.maxParticles -= particlesCollected;
        }
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }

    
}

