using UnityEngine;
using System.Collections;

public class FixedFollow : MonoBehaviour 
{
    public Quaternion parentsRotation;
    public float angle;
    public Vector3 axis,parentsEuler;
    public Transform parentTransform;

	// Update is called once per frame
	void LateUpdate () 
    {
        parentTransform = transform.parent.transform;
        parentsRotation = parentTransform.rotation;
        parentsEuler = parentsRotation.eulerAngles;
        transform.parent.transform.rotation.ToAngleAxis(out angle, out axis);
        this.transform.rotation.eulerAngles.Set(-parentsEuler.x,-parentsEuler.y,-parentsEuler.z);
    }
}
