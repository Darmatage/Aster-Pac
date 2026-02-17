using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCube_Explode : MonoBehaviour
{
	public int playerNum =1;
	public GameObject boomVFX_PS;
	public GameObject playerArt;

	public GameObject cameraFollow;

    void Start()
	{
		
	}

    void Update()
    {
        
    }

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.layer != LayerMask.NameToLayer("YourLayerName"))
		{
			GetComponent<BoxCollider>().enabled=false;
			playerArt.SetActive(false);
			GameObject boomVFX = Instantiate(boomVFX_PS, transform.position, Quaternion.identity);
			StartCoroutine(DestroyVFX(boomVFX));
		}
	}

	IEnumerator DestroyVFX(GameObject boomVFX)
	{
		if (cameraFollow.GetComponent<CameraFollow>() != null){
		cameraFollow.GetComponent<CameraFollow>().target = null;
		}
		else
		{
			cameraFollow.GetComponent<CameraFollow_MoveRotate>().playerObj = null;
		}


		yield return new WaitForSeconds(1);
		Destroy(boomVFX);

		Destroy(gameObject);
	}

}
