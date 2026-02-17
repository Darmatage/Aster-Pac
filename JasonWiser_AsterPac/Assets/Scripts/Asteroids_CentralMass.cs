using UnityEngine;

public class Asteroids_CentralMass : MonoBehaviour
{

	//Central Mass:
	Vector3 massRotation = new Vector3 (1, 3, 2);
	
	//asteroid spawning:
	public GameObject asteroid;
	public int asteroidNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartAsteroids();
    }

    // Update is called once per frame
    void Update()
    {
		//icentral mass: rotate slowly around self
        transform.Rotate (massRotation * Time.deltaTime); 
    }

	void StartAsteroids()
	{
		for (int i =0; i < asteroidNum; i++)
		{
			GameObject newRoid = Instantiate(asteroid, transform.position, Quaternion.identity);
			float newScale = Random.Range(4, 12);
			newRoid.GetComponent<Asteroid_Movement>().roidRock.transform.localScale = new Vector3(newScale *2, newScale, newScale);
		}	
	}

}