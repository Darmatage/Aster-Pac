using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject target;
	public float camSpeed = 4.0f;

	public Vector3 offsetCam; 

	void Start(){
            //target = GameObject.FindWithTag("Player");
	}

	void FixedUpdate () {
		if (target != null){
            Vector3 pos = Vector3.Lerp (transform.position, target.transform.position, camSpeed * Time.fixedDeltaTime);
            transform.position = new Vector3 (pos.x + offsetCam.x, pos.y + offsetCam.y, pos.z + offsetCam.z);
			//transform.position = new Vector3 (pos.x, pos.y, pos.z);
			transform.LookAt (target.transform.position);
		}
	}
}