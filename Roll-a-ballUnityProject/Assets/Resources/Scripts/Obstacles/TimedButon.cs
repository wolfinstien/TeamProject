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

	// Use this for initialization
	void Start () {
		active = false;
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
				}
			}
		}
	}

	void Activate()
	{
		active = true;
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
