using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Parry : MonoBehaviour {

    public Slider parrySlider;
    public bool refill = false;
    public bool deplete = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    { 
        if(refill)
        {
            parrySlider.value += Time.deltaTime;
            if(parrySlider.value >= 0.5 && !PlayerController.Instance.CanParry)
            {
                PlayerController.Instance.CanParry = true;
            }

            if(parrySlider.value >= parrySlider.maxValue)
            {
                refill = false;
            }
        }
        else if(deplete)
        {
            parrySlider.value -= Time.deltaTime;
            if (parrySlider.value <= 0)
            {
                deplete = false;
                PlayerController.Instance.CanParry = false;
            }
        }
	}
}
