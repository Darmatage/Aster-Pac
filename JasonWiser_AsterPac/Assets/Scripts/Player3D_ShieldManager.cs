using UnityEngine;

public class Player3D_ShieldManager : MonoBehaviour
{
	public Transform shieldHolder;
	public GameObject cubeShield;
	Vector3[] shieldLocations = new Vector3[125];
	int[] nums25 = {0, 1, 2, -1, -2};
	int[] shieldExists = new int[125];
	int shieldNumMax = 125; //(for a 5x5 grid)
	public int shieldNum = 0; //to track the number of active sheilds

	bool newShields = true;

	public int playerNUm = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		if (newShields)
		{
			MakeAllShields();
		}
    }

    // Update is called once per frame
    void Update()
    {
        //input to add missing blocks (for testing)
		if (Input.GetKeyDown("b"))
		{
			AddShieldCube();
		}
    }

	//initialize ShieldCubes in three steps 
	void MakeAllShields()
	{
		//STEP 1: reset shieldExists array with empty slots 
		for (int i = 0; i < shieldNumMax; i++)
		{
			shieldExists[i] = 0;
		}

		//STEP 2: populate shieldLocations array with desired 125 positions for a 5x5x5 grid:
		int Ycount = 0; //to iterate the Y value after each 25 locations
		for (int i = 0; i < shieldNumMax; i+=25)
		{ 	
			int loopCount = 0; //to iterate the shield number after each 5 locations:
			for (int posX = 0; posX < 5; posX++){
				shieldLocations[i + loopCount] = new Vector3(nums25[posX], nums25[Ycount], 0);
				shieldLocations[i+1 + loopCount] = new Vector3(nums25[posX], nums25[Ycount], 1);
				shieldLocations[i+2 + loopCount] = new Vector3(nums25[posX], nums25[Ycount], 2);
				shieldLocations[i+3 + loopCount] = new Vector3(nums25[posX], nums25[Ycount], -1);
				shieldLocations[i+4 + loopCount] = new Vector3(nums25[posX], nums25[Ycount], -2);
				loopCount += 5;
			}
			Ycount ++;
		}
		Ycount = 0;


		//STEP 3: create the ShieldCubes, starting at item 1 (no extra cube at center)
		for (int i = 1; i < shieldNumMax; i++)
		{
			GameObject newShield = Instantiate (cubeShield, 
			transform.position + shieldLocations[i], 
			Quaternion.identity);
			newShield.transform.parent = shieldHolder;
			newShield.GetComponent<ShieldCube_Explode>().shieldCubeID = i;
			newShield.GetComponent<ShieldCube_Explode>().shieldManager = gameObject;
			shieldExists[i] = 1;
		}
		CountShields();
	}

	//function to count current number of shields
	void CountShields()
	{
		//reset the shield number
		shieldNum = 0;

		//add all existing shields (never count location at 0): 
		for (int i = 1; i < shieldNumMax; i++)
		{
			shieldNum += shieldExists[i];
		}
		Debug.Log("Number of shields = " + shieldNum);
		Debug.Log("Shield locations: " + string.Join(", ", shieldLocations));
	}

	//function to add a cube when materials are ready, sent by engineer
	public void AddShieldCube()
	{
		//look through shields to try to find an empty one: 
		for (int i = 1; i < shieldNumMax; i++){
			if (shieldExists[i] == 0)
			{
				GameObject newShield = Instantiate (cubeShield, 
					transform.position + shieldLocations[i], 
					Quaternion.identity);
				newShield.transform.parent = shieldHolder;
				newShield.GetComponent<ShieldCube_Explode>().shieldCubeID = i;
				shieldExists[i] = 1;
				Debug.Log("Added a cube at location: " + shieldExists[i]);
				return;
			}
		}
		Debug.Log("no empty spaces to add a cube");
		//and reimburse the cost fot he shieldcube
	}

	//function to remove a destroyed cube, sent by ShieldCube being destroyed 
	public void RemoveShieldCube(int cubeID)
	{
		//set destroyed cube slot to empty:
		shieldExists[cubeID] = 0;
		Debug.Log("player #" + playerNUm + " lost a cube at location: " + cubeID);
	}

}
