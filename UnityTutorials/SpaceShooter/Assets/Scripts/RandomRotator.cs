using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour 
{
	public float tumble;

	void Start()
	{
		Rigidbody rigidBody = GetComponent<Rigidbody>();
		rigidBody.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
