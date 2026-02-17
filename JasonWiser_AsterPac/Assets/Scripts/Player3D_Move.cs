using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player3D_Move : MonoBehaviour
{
	public float rotationSpeed = 5f;
	public float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) //accelerate
		{
			if (moveSpeed <= 1)
			{
				moveSpeed = 40;
			}
		}
		if (Input.GetKeyDown(KeyCode.G)) //accelerate
		{
			if (moveSpeed > 0)
			{
				moveSpeed = 1;
			}
		}
    }

	// Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }

	private void ShipMovement()
	{
		//pitch (up and down)
		float pitch = rotationSpeed * Time.deltaTime * Input.GetAxis("Pitch");
		//Yaw (left and right)
		float yaw = rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
		//Roll
		float roll = rotationSpeed * Time.deltaTime * Input.GetAxis("Roll");

		//implements rotation:
		transform.Rotate(pitch, yaw, roll);
	}

}

/*
https://medium.com/unity-coder-corner/coding-flight-controls-in-c-88a16326b95a
Input manager:

"Pitch"
Buttons: down, up, s, w
Gravity = 3, dead = 0.001, sensitivity = 3, snap = enabled

for the yaw:
"Horizontal"
Buttons: left, right, a, d
Gravity = 3, dead = 0.001, sensitivity = 3, snap = enabled

"Roll"
Buttons: e, q
Gravity = 3, dead = 0.001, sensitivity = 3, snap = enabled

*/