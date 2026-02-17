using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldCube_Explode : MonoBehaviour
{
	public int shieldCubeID;
	public string shieldCubeLayer = "p1_ShieldCube";
	public GameObject boomVFX_PS;
	public GameObject shieldCubeArt;
	public GameObject shieldManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
	{
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnCollisionEnter(Collision other)
	{
		Debug.Log("shield cube #" + shieldCubeID + "hit something");
		if (other.gameObject.layer != LayerMask.NameToLayer("YourLayerName"))
		{
			shieldManager.GetComponent<Player3D_ShieldManager>().RemoveShieldCube(shieldCubeID);
			GetComponent<BoxCollider>().enabled=false;
			shieldCubeArt.SetActive(false);
			GameObject boomVFX = Instantiate(boomVFX_PS, transform.position, Quaternion.identity);
			StartCoroutine(DestroyVFX(boomVFX));
		}
	}

	IEnumerator DestroyVFX(GameObject boomVFX)
	{
		yield return new WaitForSeconds(2);
		Destroy(boomVFX);
		Destroy(gameObject);
	}

}
