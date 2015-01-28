using UnityEngine;
using System.Collections;

public class PushBlock : MonoBehaviour {
	public int maxDistance;
	private Vector3 startPos;
	// Use this for initialization
	void Start () {
		startPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (this.transform.position.z <= startPos.z && this.transform.position.z > (this.startPos.z + maxDistance))
			{
				rigidbody.AddForce(0.0f,0.0f, 1.0f * Time.deltaTime);
			}
		}
	}
}