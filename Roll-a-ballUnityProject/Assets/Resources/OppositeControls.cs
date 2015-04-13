using UnityEngine;
using System.Collections;

public class OppositeControls : MonoBehaviour 
{
    float currentSpeed;
    float maxSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            /* 
         * Check for player keyboard input and move ball accordingly
         */
            maxSpeed = 12.5f;
            if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    GetComponent<Rigidbody>().AddForce(Vector3.forward * -maxSpeed);
                }
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    GetComponent<Rigidbody>().AddForce(Vector3.back * -maxSpeed);
                }
                if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow))
                {
                    GetComponent<Rigidbody>().AddForce(Vector3.left * -maxSpeed);
                }
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    GetComponent<Rigidbody>().AddForce(Vector3.right * -maxSpeed);
                }
            }
#endif

#if UNITY_ANDROID
            /*
             * Move ball based on accelerometer values
             */
            Vector3 movement = new Vector3(Input.acceleration.x, 0f, Input.acceleration.y);

            if (movement.sqrMagnitude > 1)
               movement.Normalize();
        
		    if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
		    {
        	    GetComponent<Rigidbody>().AddForce(movement * -50f);
		    }
#endif
        }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            this.GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;
            other.gameObject.SetActive(false);
        }
    }
}
