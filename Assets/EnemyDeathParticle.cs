using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathParticle : MonoBehaviour
{
    ParticleSystemRenderer particle;
    ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particle = GetComponent<ParticleSystemRenderer>();

        ChangeColor();
    }

    private void ChangeColor()
    {
        if (!EnemyManager.goalReached)
        {
            particleSystem.startColor = ColorSetter.instance.GetBaseColor;
            particle.material.color = ColorSetter.instance.GetBaseColor;
        }
        else
        {
            particleSystem.startColor = ColorSetter.instance.GetGodModeColor(true);
            particle.material.color = ColorSetter.instance.GetGodModeColor(true);
        }
    }
}
