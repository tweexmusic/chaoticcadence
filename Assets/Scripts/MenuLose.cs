using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLose : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float windowDelayTimer;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

}
