using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	private Vector3 posA; 
	private Vector3 posB;
	private Vector3 nextPos;


	public float moveSpeed;
	public Transform child;
	public Transform transformB;
	// Use this for initialization
	void Start () {
		posA = child.localPosition;
		posB = transformB.localPosition;
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

	private void Changedestination()
	{
		nextPos = nextPos == posB ? posA : posB;
	}

}
