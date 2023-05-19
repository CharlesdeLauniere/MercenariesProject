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
		slider.value = currentMana;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
	public void SetMaxMana(float maxMana)
	{
        slider.maxValue = maxMana;
		slider.value = maxMana;
        fill.color = gradient.Evaluate(1f);
		
	}


}
