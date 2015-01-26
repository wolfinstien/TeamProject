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
        
        #if UNITY_STANDALONE_WIN

        /* 
         * Check for player keyboard input and move ball accordingly
         */
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			rigidbody.AddForce(Vector3.forward*10);
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			rigidbody.AddForce(Vector3.back*10);
		}
		if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow))
		{
			rigidbody.AddForce(Vector3.left*10);
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			rigidbody.AddForce(Vector3.right*10);
		}

        /*
         * Check for jump
         */
        if (Input.GetKeyDown(KeyCode.Space)) {
            rigidbody.AddForce(new Vector3(0f, 3.25f, 0f), ForceMode.Impulse);
        }

        #endif

        #if UNITY_ANDROID

        Vector3 dir = Vector3.zero;

        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;

        rigidbody.transform.Translate(dir * 10f);

        #endif
	}
}
