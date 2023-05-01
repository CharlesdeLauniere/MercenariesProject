using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetHealth(float currentHealth)
    {
		Debug.Log("sethealthbar");
		
		StartCoroutine(waitToRemoveHealth(slider,currentHealth));
		
	}

	IEnumerator waitToRemoveHealth(Slider slider, float currentHealth)
	{
		yield return new WaitForSeconds(2f);
        slider.value = currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
	public void SetMaxHealth(float maxHealth)
	{
        Debug.Log("maxsethealthbar");
        slider.maxValue = maxHealth;
		slider.value = maxHealth;
        fill.color = gradient.Evaluate(1f);
		
	}


}
