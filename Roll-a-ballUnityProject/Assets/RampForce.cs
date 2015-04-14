using UnityEngine;
using System.Collections;

public class RampForce : MonoBehaviour 
{
    public float magnitudeForce;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ramp!");
        other.GetComponent<Rigidbody>().AddForce(this.transform.forward*-magnitudeForce);
    }
}
