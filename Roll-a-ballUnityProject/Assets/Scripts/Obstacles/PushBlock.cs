using UnityEngine;
using System.Collections;

public class PushBlock : MonoBehaviour 
{
	public float maxDistance;
	public float minDistance;
	public float startPos;

    // Use this for initialization
	void Start () 
	{
		startPos = transform.position.z;
	}

	void FixedUpdate()
	{
		if (transform.position.z<startPos-minDistance)
		{
			Debug.Log ("before");
			gameObject.transform.position.Set(0,0,startPos-minDistance);

		}
		else if (this.transform.position.z>startPos+maxDistance)
		{
			Debug.Log("past");
			gameObject.transform.position.Set(0,0,startPos+maxDistance);
		}
	}
}