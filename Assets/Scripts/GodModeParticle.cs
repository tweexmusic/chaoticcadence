using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodModeParticle : MonoBehaviour
{
    ParticleSystemRenderer particle;
    ParticleSystem particleSystem;


    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particle = GetComponent<ParticleSystemRenderer>();
        particleSystem.startColor = ColorSetter.instance.GetGodModeColor(true);
        particle.material.color = ColorSetter.instance.GetGodModeColor(true);
    }

    private void Update()
    {
        DestroyDeathParticle();
    }

    private void DestroyDeathParticle()
    {
        if (particleSystem.isStopped)
        {
            Destroy(gameObject);
        }
    }
}