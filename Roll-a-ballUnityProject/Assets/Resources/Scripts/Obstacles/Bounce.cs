using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour 
{
	public float mForce;

	void OnCollisionEnter(Collision other)
	{
		other.rigidbody.AddExplosionForce (mForce, this.transform.position, 2);
	}
}
