using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;
    public Transform shotSpawnLeft;
    public Transform shotSpawnRight;
	public float fireRate;

	private float nextFire;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
	{
		AudioSource audio = GetComponent<AudioSource>();
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
            if (GameController.level >= 10)
            {
                // The left and right shots' rotations have to be opposite of the ship's tilt or they just fly off the screen.
                Instantiate(shot, shotSpawnLeft.position, Quaternion.Euler(0.0f, -10, rigidBody.velocity.x * tilt));
                Instantiate(shot, shotSpawnRight.position, Quaternion.Euler(0.0f, 10, rigidBody.velocity.x * tilt));
            }
            audio.Play();
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement =  new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidBody.velocity = movement * speed;

		rigidBody.position = new Vector3
		(
			Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
		);
        //  Tilt
		rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * -tilt);
	}
}
