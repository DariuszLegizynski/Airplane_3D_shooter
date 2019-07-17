using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrottlePercentageSpeed : MonoBehaviour {

    Text sliderText;
    public int sliderValue;

	// Use this for initialization
	void Start ()
    {
        sliderText = GetComponent<Text>();
	}
	
    public void TextUpdate (float value)
    {
        sliderValue = Mathf.RoundToInt(value * 100);
        sliderText.text = sliderValue + "%";
    }
}
