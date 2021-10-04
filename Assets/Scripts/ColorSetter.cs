using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    public static ColorSetter instance;

    //public ColorSO baseColors;

    public Color baseColor;
    public Color playerGodModeColor;
    public Color enemeyGodModeColor;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Color GetBaseColor
    {
        get
        {
            //Color newColor = new Color(baseColors.redValue / 255, baseColors.greenValue / 255, baseColors.blueValue / 255);
            //return newColor;
            return baseColor;
        }
    }

    public Color GetGodModeColor(bool colorForPlayer)
    {
        //Color newColor = new Color(239f / 255, 243f / 255, 0f / 255);
        //return newColor;

        if (colorForPlayer)
        {
            return playerGodModeColor;
        }
        else
        {
            return enemeyGodModeColor;
        }
    }
}
