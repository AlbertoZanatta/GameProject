using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	private Vector3 posA; 
	private Vector3 posB;
	private Vector3 posC;
	private Vector3 posD;
	private Vector3 nextPos;

	public float moveSpeed;
	public Transform child;
	public Transform transformB;
	public Transform transformC;
	public Transform transformD;
	// Use this for initialization
	void Start () {
        // GameObject GM = GameObject.Find("Final platform");
		// if (GM.name == "Final platform")
		// {
		// 	moveSpeed = 0;
		// }
		posA = child.localPosition;
		posB = transformB.localPosition;
		if (transformC != null)
		{
			posC = transformC.localPosition;
		}
		if (transformD != null)
		{
			posD = transformD.localPosition;
		}

		nextPos = posB;
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	private void Move()
	{
		child.localPosition = Vector3.MoveTowards(child.localPosition, nextPos, moveSpeed * Time.deltaTime);

		if (Vector3.Distance(child.localPosition, nextPos) <= 0.1)
		{
			 Changedestination();
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			moveSpeed = 4;
		}
	}

	private void Changedestination()
	{
		if (transformC != null)
		{
			if (transformD != null)
			{
				if (nextPos == posB)
				{
					nextPos = posC;
				}
				else if (nextPos == posC)
				{
					nextPos = posD;
				}
				else if (nextPos == posD)
				{
					nextPos = posA;
				}
				else
				{
					nextPos = posB;
				}
			}
		}
		else
		{
			nextPos = nextPos == posB ? posA : posB;
		}
	}

}
