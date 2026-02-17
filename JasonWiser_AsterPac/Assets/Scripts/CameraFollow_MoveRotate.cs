using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_MoveRotate : MonoBehaviour {

[SerializeField]
    public Transform playerObj;
    public float speed = 0.01f;

[SerializeField]
    public Vector3 offsetCam;
    Vector3 offsetCameraSide;

[SerializeField]
    private Space offsetPositionSpace = Space.Self;

[SerializeField]
    private bool lookAt = true;
    public bool isPlayer1 = true;

    float theHeight = 4f;
    float heightChange = 0.1f;
    float heightMin = 0.2f;
    float heightMax = 10f;

//LERP:
   Vector3 newPos;
    public float smoothLERP = 5f;
    private float t = 0f;

//smoother LERP
    float elapsedTime = 0.0f;
    float duration = 2.0f; // Total time the SLERP should take

    void Start(){
        //offsetCam = new Vector3(5, 4, -9);
    }

    void Update(){
        //camera Y-height controls:
        if (isPlayer1){

            if (Input.GetKeyDown(KeyCode.LeftShift)){
                theHeight = 4f;
                SetCameraHeight();
            }
            else if (Input.GetKey(KeyCode.LeftShift)){
                theHeight += heightChange;
                SetCameraHeight();
            }

            else if (Input.GetKey(KeyCode.LeftControl)){
                theHeight -= heightChange;
                SetCameraHeight();
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.RightShift)){
                theHeight = 4f;
                SetCameraHeight();
            }
            else if (Input.GetKey(KeyCode.RightShift)){
                theHeight += heightChange;
                SetCameraHeight();
            }

            else if (Input.GetKey(KeyCode.RightControl)){
                theHeight -= heightChange;
                SetCameraHeight();
            }
        }
    }

    void SetCameraHeight() {
        if (theHeight < heightMin){theHeight = heightMin;}
        else if (theHeight > heightMax){theHeight = heightMax;}
        offsetCam = new Vector3(offsetCam.x, theHeight, offsetCam.z);
        //Debug.Log("current offset cam: " + offsetCam + ", isPlayer1?: " + isPlayer1);
    }

    private void FixedUpdate() {
		if (playerObj != null){
			MoveAndRotateCamera();
			//LINEAR:
			//Vector3 LERP_Pos = Vector3.Lerp (transform.position, newPos, smoothLERP * Time.deltaTime);
			//transform.position = LERP_Pos;

			//CIRCULAR:
			// Calculate the interpolation factor over time
			// t += Time.fixedDeltaTime * smoothLERP;
			// Clamp t between 0 and 1 for a single movement, or use Mathf.PingPong for back and forth
			// t = Mathf.Clamp01(t);

			// SMOOTHER LERP: Increment the elapsed time
			elapsedTime += Time.deltaTime;

			// Calculate the interpolation point (t)
			float t = Mathf.Clamp01(elapsedTime / duration);

			// Apply smooth step for a natural slow down at the end
			t = Mathf.SmoothStep(0.0f, 1.0f, t);

			// Transform points into local space relative to the center
			Vector3 localStart = transform.position - playerObj.position;
			Vector3 localEnd = newPos - playerObj.position;

			// Perform the spherical LERP Slerp in local space
			//Vector3 localInterpolated = Vector3.Slerp(localStart, localEnd, t);
			Vector3 localInterpolated = Vector3.Slerp(transform.position, localEnd, t);

			// Convert back to world space
			transform.position = newPos + localInterpolated;
		}
    }

   public void MoveAndRotateCamera() {
        //offsetCameraSide = new Vector3(offsetCam.x, offsetCam.y, offsetCam.z +1);
        //if (playerObj == null) { Debug.LogWarning("Missing playerObj ref !", this); return; }

		// compute position
		if (playerObj != null){
			if (offsetPositionSpace == Space.Self) {
				newPos = playerObj.TransformPoint(offsetCam);
			}
			else {
				newPos = playerObj.position + offsetCam;
			}

			// compute rotation
			if (lookAt) {
				transform.LookAt(playerObj);
			}
			else {
				Transform fromRot = gameObject.transform;
				Quaternion toRot = Quaternion.Euler(playerObj.rotation.x, playerObj.rotation.y, playerObj.rotation.z);
				transform.rotation = Quaternion.Lerp(fromRot.rotation, toRot, Time.time * speed);
			}
		}
    }
}