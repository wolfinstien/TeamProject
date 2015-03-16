using UnityEngine;
using System.Collections;

public class ColourChange : MonoBehaviour {

	public string colour;

	// Use this for initialization
	void Start () {
		switch (colour)
		{
		case "Red":
			this.GetComponent<Renderer>().material.color = Color.red;
			break;

		case "Blue":
			this.GetComponent<Renderer>().material.color = Color.blue;
			break;

		case "Green":
			this.GetComponent<Renderer>().material.color = Color.green;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
