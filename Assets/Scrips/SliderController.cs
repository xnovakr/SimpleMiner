using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Text Text;
    public int currentValue;

    public GameSettings gameSettings;
    public GeneratorSettings generatorSettings;

    private void LateUpdate()
    {
        currentValue = Mathf.RoundToInt(gameObject.GetComponent<Slider>().value);
        Text.text = "" + currentValue;
        SetCunksNumber(currentValue);
    }

    public int GetValue()
    {
        return currentValue;
    }

    public void SetRange(int start, int stop)
    {
        gameObject.GetComponent<Slider>().minValue = start;
        gameObject.GetComponent<Slider>().maxValue = stop;
    }

    public void SetCunksNumber(int number)
    {
        generatorSettings.numberOfChunks = number;
    }
}
