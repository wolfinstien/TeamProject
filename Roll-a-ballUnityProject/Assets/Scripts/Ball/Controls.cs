using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

    #region Fields

    private const float FORCE = 120f;

    #endregion

    #region Functions

    // Use this for initialization
	void Start () 
	{
        DontDestroyOnLoad(transform.gameObject);
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
			rigidbody.AddForce(Vector3.forward * FORCE);
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
            rigidbody.AddForce(Vector3.back * FORCE);
		}
		if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow))
		{
            rigidbody.AddForce(Vector3.left * FORCE);
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
            rigidbody.AddForce(Vector3.right * FORCE);
		}

        /*
         * Check for jump
         */
        if (Input.GetKeyDown(KeyCode.Space)) 
            rigidbody.AddForce(new Vector3(0f, 3.25f, 0f), ForceMode.Impulse);

        #endif

        #if UNITY_ANDROID

        /*
         * Move ball based on accelerometer values
         */
        Vector3 movement = new Vector3(Input.acceleration.x, 0f, Input.acceleration.y); //-y

        if (movement.sqrMagnitude > 1)
            movement.Normalize();
        
        rigidbody.AddForce(movement * 15f);

        /*
         * Tap screen to jump
         */
        if (Input.GetMouseButtonDown(0))
            rigidbody.AddForce(new Vector3(0f, 3.25f, 0f), ForceMode.Impulse);

        #endif
    }

    #endregion
}
