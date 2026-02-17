using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyThis : MonoBehaviour
{
	public float timeToDestroy = 4f;

    void Start()
    {
        StartCoroutine(DestroyObject());
    }

    void Update()
    {
        
    }

	IEnumerator DestroyObject()
	{
		yield return new WaitForSeconds(timeToDestroy);
		Destroy(gameObject);
	}

}
