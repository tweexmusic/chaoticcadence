using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Values")]
public class ColorSO : ScriptableObject
{
    public float redValue = 0;
    public float greenValue = 0;
    public float blueValue = 0;
    [Range(0,1)]
    public float alpha = 1;
}
