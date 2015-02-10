using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	public GameObject[] door;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	// Should probably use fixed update here as it is for physics/aimation
	void Update () 
	{

	}

	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("button pressed");

		for (int i=0; i<8; i++) 
		{
			door = GameObject.FindGameObjectsWithTag("Door");
			Debug.Log(door[i].transform.position);
			Vector3 open = door[i].transform.position;
			//open door
			open.y += 3;
			door[i].transform.position = open;//(open.x, open.y, open.z);
		}
	}
}
