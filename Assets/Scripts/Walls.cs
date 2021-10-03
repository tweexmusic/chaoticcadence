using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    SpriteRenderer wallSpriteRender;


    private void Awake()
    {
        wallSpriteRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!EnemyManager.goalReached)
        {
            //SetWallColor(ColorSetter.instance.redValue, ColorSetter.instance.greenValue, ColorSetter.instance.blueValue);
        }
    }

    public void SetWallColor(int red, int green, int blue)
    {
        wallSpriteRender.color = new Color(red, green, blue);
    }
}
