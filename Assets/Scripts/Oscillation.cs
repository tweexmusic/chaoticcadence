using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    Vector2 startingPosition;

    [SerializeField]
    Vector2 movementVector;
    float movementFactor;
    //[SerializeField]
    //float frequency = 2f;

    [SerializeField]
    float tempo;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        SetRandomMovementVector();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetFrequency > Mathf.Epsilon)
        {
            float cycles = Time.time / GetFrequency;

            const float tau = Mathf.PI * 2;
            float rawSinWave = Mathf.Sin(cycles * tau);

            //movementFactor = (rawSinWave + 1f) / 2f;

            Vector2 offset = movementVector * rawSinWave;
            transform.position = startingPosition + offset;
        }
        
    }

    private void SetRandomMovementVector()
    {
        movementVector.y = SetVectors.yVector;
        movementVector.x = SetVectors.xVector;

    }

    public float GetFrequency
    {
        get
        {
            return (tempo / 60) * 2;
        }
    }
}
