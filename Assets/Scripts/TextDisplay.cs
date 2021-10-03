using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public string textValue;
    public Text textElement;

    // Update is called once per frame
    void Update()
    {
        textElement.text = textValue.ToString();
    }
}
