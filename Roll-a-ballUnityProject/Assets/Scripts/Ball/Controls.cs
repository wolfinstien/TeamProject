using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Physics.gravity.Set(0.0f,-1000f,0.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
        
        #if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

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

        #endif

        #if UNITY_ANDROID

        /*
         * Move ball based on accelerometer values
         */
        Vector3 movement = new Vector3(Input.acceleration.x, 0f, Input.acceleration.y); //-y

        if (movement.sqrMagnitude > 1)
            movement.Normalize();
        
        rigidbody.AddForce(movement * 150f);

        #endif
	}

	void OnCollisionEnter()
	{
		#if UNITY_ANDROID
		Handheld.Vibrate ();
		#endif
	}
}
