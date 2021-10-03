using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    ParticleSystem particle;
    Material particleMaterial;


    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        particleMaterial = GetComponent<Material>();
        //particleMaterial.color = new Color(ColorSetter.instance.redValue, ColorSetter.instance.greenValue, ColorSetter.instance.blueValue);
    }

    private void Update()
    {
        DestroyDeathParticle();
    }

    private void DestroyDeathParticle()
    {
        if (particle.isStopped)
        {
            Destroy(gameObject);
        }
    }
}