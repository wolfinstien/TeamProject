using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	public GameObject door[];
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	// Should probably use fixed update here as it is for physics/aimation
	void Update () 
	{

	}

	void OnCollisionEnter()
	{
		Debug.Log ("button pressed");

		for (int i=0; i<4; i++) 
		{
			door = GameObject.FindGameObjectsWithTag("Door");
			Debug.Log(door[0].transform.position);
			Vector3 closed = door[0].transform.position;
			//open door
			closed.y += 3;
			door[0].transform.position.Set (closed.x, closed.y, closed.z);
		}
	}
}
