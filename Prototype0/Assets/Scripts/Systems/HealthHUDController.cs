using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUDController : MonoBehaviour {

    public Health health;
    public Slider healthSlider;

	// Use this for initialization
	void Start () {
        health.healthChange += Health_healthChange;
        healthSlider.maxValue = health.MaxHealth;
	}

    private void Health_healthChange(object sender, HealthEventArgs e)
    {
        int newhealthValue = e.healthPoints > 0 ? e.healthPoints : 0;
        healthSlider.value = newhealthValue;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
