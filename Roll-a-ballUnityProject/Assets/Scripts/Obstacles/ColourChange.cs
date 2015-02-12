using UnityEngine;
using System.Collections;

public class ColourChange : MonoBehaviour {

	public string colour;

	// Use this for initialization
	void Start () {
		switch (colour)
		{
		case "Red":
			this.renderer.material.color = Color.red;
			break;

		case "Blue":
			this.renderer.material.color = Color.blue;
			break;

		case "Green":
			this.renderer.material.color = Color.green;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
