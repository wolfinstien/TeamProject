using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.W))
		{
			rigidbody.AddForce(Vector3.forward*10);
		}
		if (Input.GetKey(KeyCode.S))
		{
			rigidbody.AddForce(Vector3.back*10);
		}
		if (Input.GetKey(KeyCode.A))
		{
			rigidbody.AddForce(Vector3.left*10);
		}
		if (Input.GetKey(KeyCode.D))
		{
			rigidbody.AddForce(Vector3.right*10);
		}
	}
}
