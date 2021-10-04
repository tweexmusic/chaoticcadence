using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    SpriteRenderer wallSpriteRender;


    private void Awake()
    {
        wallSpriteRender = GetComponent<SpriteRenderer>();
        EnemyManager.OnLevelGoalReached += OnLevelGoalReached;
    }

    private void Start()
    {
        wallSpriteRender.color = ColorSetter.instance.GetBaseColor;
    }

    private void OnDestroy()
    {
        EnemyManager.OnLevelGoalReached -= OnLevelGoalReached;
    }

    private void OnLevelGoalReached()
    {
        wallSpriteRender.color = ColorSetter.instance.GetGodModeColor(true);
    }
}
