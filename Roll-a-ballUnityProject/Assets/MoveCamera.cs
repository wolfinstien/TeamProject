using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	GameObject mCamera;
	Vector3	newCameraPosition;
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}

	void OnTriggerEnter()
	{
		mCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		newCameraPosition = this.transform.position;
		newCameraPosition.y += 20;
		mCamera.transform.position = newCameraPosition;
	}
}
