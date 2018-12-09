using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : MonoBehaviour, Damageable {

    public Sprite[] brokenSteps;
    [SerializeField] private int healthPoints;


    private SpriteRenderer spriteRenderer;
    private int currentHealthPoints;

    // Use this for initialization
    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthPoints = brokenSteps.Length;
        Debug.Log("Health points: " + healthPoints + ", array length: " + brokenSteps.Length);
        currentHealthPoints = healthPoints;
        spriteRenderer.sprite = brokenSteps[healthPoints - currentHealthPoints];
	}

    public void Hit(Weapon weapon)
    {
        currentHealthPoints--;
        if(currentHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            
            spriteRenderer.sprite = brokenSteps[healthPoints - currentHealthPoints];
        }
    }
}
