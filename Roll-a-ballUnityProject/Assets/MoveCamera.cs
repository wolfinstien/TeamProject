using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	GameObject mCamera;
	Vector3	newCameraPosition;

	void OnTriggerEnter()
	{
		mCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		newCameraPosition = this.transform.position;
		newCameraPosition.y += 20;
		mCamera.transform.position = newCameraPosition;
		CubeMatrix switchRooms = new CubeMatrix ();
		switchRooms.StartShift ();
	}
}
