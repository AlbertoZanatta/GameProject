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
           
            gameObject.SetActive(false);
        }
        else
        {

            spriteRenderer.sprite = brokenSteps[healthPoints - currentHealthPoints];
        }
    }

    public void Reset()
    {
        timeElapsed = 0;
        currentHealthPoints = healthPoints;
        spriteRenderer.sprite = brokenSteps[0];
    }

}
