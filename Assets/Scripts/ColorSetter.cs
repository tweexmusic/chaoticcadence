using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    public static ColorSetter instance;

    public int redValue;
    public int greenValue;
    public int blueValue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
