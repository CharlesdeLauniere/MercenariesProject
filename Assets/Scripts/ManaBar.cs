using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ManaBar : MonoBehaviour
{

	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMana(float currentMana)
    {
		Debug.Log("setmanabar");
		slider.value = currentMana;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
	public void SetMaxHealth(float maxMana)
	{
        Debug.Log("maxsethealthbar");
        slider.maxValue = maxMana;
		slider.value = maxMana;
        fill.color = gradient.Evaluate(1f);
		
	}


}
