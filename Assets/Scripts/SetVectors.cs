using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVectors : MonoBehaviour
{
    public static int xVector;
    public static int yVector;

    private void Awake()
    {
        xVector = Random.Range(0, 8);
        yVector = Random.Range(0, 4);
    }
}
