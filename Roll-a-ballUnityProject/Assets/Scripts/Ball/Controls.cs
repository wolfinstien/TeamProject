using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour 
{
	public float maxSpeed;
	private bool inAir;
	private float distToGround;

	// Use this for initialization
	void Start () 
	{
		//Physics.gravity.Set(0.0f,-1000f,0.0f);
		distToGround = collider.bounds.extents.y;
	}

	// Update is called once per frame
	void Update () 
	{

        #if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

        /* 
         * Check for player keyboard input and move ball accordingly
         */

		if (rigidbody.velocity.magnitude < maxSpeed && IsGrounded())
		{
        	if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				rigidbody.AddForce(Vector3.forward * maxSpeed);
			}
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				rigidbody.AddForce(Vector3.back * maxSpeed);
			}
			if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow))
			{
				rigidbody.AddForce(Vector3.left * maxSpeed);
			}
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				rigidbody.AddForce(Vector3.right * maxSpeed);
			}
		}

        #endif

        #if UNITY_ANDROID

        /*
         * Move ball based on accelerometer values
         */
        Vector3 movement = new Vector3(Input.acceleration.x, 0f, Input.acceleration.y); //-y

        if (movement.sqrMagnitude > 1)
            movement.Normalize();
        
		if (rigidbody.velocity.magnitude < maxSpeed || inAir)
		{
        	rigidbody.AddForce(movement * 150f);
		}

        #endif
	}

	bool IsGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}

	//added by adam
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickup")
		{
			this.renderer.material.color = other.renderer.material.color;
			other.gameObject.SetActive(false);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		// todo: there is a problem with the relative position where it is taking the 
		//local y coordinate of the sphere to do the above/bellow calculation meaning 
		//sometimes it triggers above and sometimes not depending on which way up the sphere is.

		Vector3 contactPoint = other.contacts[0].point;
		var relativePosition = transform.InverseTransformPoint(contactPoint);
		//Debug.Log ("CP= " + contactPoint + " RP= " + relativePosition);

		// dont want to colide with objects we are rollling on
		if (!(relativePosition.y > 0))
		{
			//Debug.Log("The object is not above.");

			// these shouldnt trigger collisions either
			if (/*other.gameObject.name != "FloorTL" &&
			    other.gameObject.name != "FloorBL" &&
			    other.gameObject.name != "FloorTR" &&
			    other.gameObject.name != "FloorBR" &&*/
			    other.gameObject.name != "CubeSlope")
			{
				//Debug.Log(other.gameObject.name);
				this.audio.Play();
				#if UNITY_ANDROID
				Handheld.Vibrate ();
				#endif
			}
		}
	}
}
