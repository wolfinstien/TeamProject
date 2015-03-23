using UnityEngine;
using System.Collections;

public class TimedButton : MonoBehaviour {
	public int duration;
	public float tick;
	public float green;
	public float red;
	private bool touched = false;
	Color moment;
	// Use this for initialization
	void Start ()
	{
		tick = 0;
		green = 1.0f;
		red = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		moment.r = red;
		moment.g = green;
		this.GetComponent<Renderer> ().material.color = moment;
		if (touched)
		{
			tick++;
			red = 1.0f * (tick / duration);
			green = 1.0f - (1.0f * (tick / duration));
			if (tick > duration)
			{
				touched = false;
				tick = 0;
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!touched) 
			{
				touched = true;
			} 
		}
	}
}
