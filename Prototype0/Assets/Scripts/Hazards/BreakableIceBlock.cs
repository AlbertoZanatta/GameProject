using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableIceBlock : MonoBehaviour {
    public Sprite[] brokenSteps;
    [SerializeField] private int healthPoints;


    private SpriteRenderer spriteRenderer;
    private int currentHealthPoints;
    [SerializeField] private float timeToDamage = 1f;
    private float timeElapsed;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthPoints = brokenSteps.Length;
        Debug.Log("Health points: " + healthPoints + ", array length: " + brokenSteps.Length);
        currentHealthPoints = healthPoints;
        spriteRenderer.sprite = brokenSteps[healthPoints - currentHealthPoints];
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= timeToDamage)
        {
            timeElapsed = 0;
            Hit();
        }
    }
    public void Hit()
    {
        currentHealthPoints--;
        if (currentHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
        else
        {

            spriteRenderer.sprite = brokenSteps[healthPoints - currentHealthPoints];
        }
    }
}
