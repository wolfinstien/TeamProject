using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));
	}
}
