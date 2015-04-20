using UnityEngine;
using System.Collections;

public class TimedButon : MonoBehaviour {
	
	public enum Affect
	{
		Kinematic,
		RotateCont,
		Rotate90
	};

	public int duration;
	public GameObject objToAffect;
	public Affect type;

	private bool active;
	private int count;
	private Animator timer;

	// Use this for initialization
	void Start () {
		active = false;
		timer = this.gameObject.transform.GetChild (0).gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (active)
		{
			count++;
			if (count > duration)
			{
				Deactivate();
			}
			else
			{
				if (type == Affect.RotateCont)
				{
					objToAffect.transform.RotateAround(objToAffect.transform.position, new Vector3(0.0f, 1.0f, 0.0f), 1.0f);
					print ("rotate: " + count + " degrees");
				}
			}
		}
	}

	void Activate()
	{
		active = true;
		timer.Play("Timer");
		timer.speed = 1.0f / duration;
		switch (type)
		{
		case Affect.Kinematic:
			objToAffect.GetComponent<Rigidbody>().isKinematic = !objToAffect.GetComponent<Rigidbody>().isKinematic;
			break;
		case Affect.Rotate90:
			objToAffect.transform.RotateAround(objToAffect.transform.position, new Vector3(0.0f, 1.0f, 0.0f), 90.0f);
			break;
		}
	}

	void Deactivate()
	{
		active = false;
		timer.Play("Stop");
		count = 0;
		switch (type)
		{
		case Affect.Kinematic:
			objToAffect.GetComponent<Rigidbody>().isKinematic = !objToAffect.GetComponent<Rigidbody>().isKinematic;
			break;
		case Affect.Rotate90:
			objToAffect.transform.RotateAround(objToAffect.transform.position, new Vector3(0.0f, 1.0f, 0.0f), -90.0f);
			break;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player" && !active)
		{
			Activate();
		}
	}
}
