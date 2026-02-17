using UnityEngine;

public class Asteroid_Movement : MonoBehaviour
{

	//Asteroid
	public GameObject roidRock;
	public Transform centerMass;
	Vector3 roidRotation;

	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		centerMass = GameObject.FindWithTag("CenterMass").transform;
		StartAsteroid();
    }

    // Update is called once per frame
    void Update()
    {
		//rotate pivot:
		//transform.Rotate (roidRotation * Time.deltaTime); 
    }

	void StartAsteroid()
	{
		//position of child:
		float rangePosX = Random.Range(30,70);
		float rangePosY = Random.Range(-12,12);
		float rangePosZ = Random.Range(30,70);
		int isNegX = Random.Range(0,2);
		int isNegZ = Random.Range(0,2);
		if (isNegX > 0) {rangePosX = rangePosX * -1;}
		if (isNegZ > 0) {rangePosZ = rangePosZ * -1;}
		Vector3 startposition = new Vector3 (rangePosX, rangePosY, rangePosZ);
		roidRock.transform.position = startposition;
		roidRock.transform.LookAt(centerMass, Vector3.up);

		//distribute angles along Y axis:
		float angleY = Random.Range(0,360);
		transform.Rotate (new Vector3(0,angleY,0)); 

		//rotation of pivot:
		float rotY = Random.Range(0,4);
		float rotX = Random.Range(0,0.2f);
		float rotZ = Random.Range(0,0.2f);
		roidRotation = new Vector3 (rotX, rotY, rotZ); 
	}

}
