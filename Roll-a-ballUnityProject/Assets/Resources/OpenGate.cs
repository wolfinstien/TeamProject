using UnityEngine;
using System.Collections;

public class OpenGate : MonoBehaviour 
{
    public bool isOpen;
    public Vector3 relativePosition;
    Vector3 targetPosition;
    Vector3 targetRotation;
    //Transform target;
    float speed = 1.0f;

	// Use this for initialization
	void Start () 
    {
        isOpen = false;
        targetPosition = this.transform.position + relativePosition;
        targetRotation = Vector3.Cross(Vector3.up,this.transform.forward);
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log(this.transform.position);
	    if(isOpen)
        {
            //Vector3 targetDir = this.transform.position + relativePosition;
            float step = speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetRotation, step, 0.0F);
            Debug.DrawRay(transform.position, newDir, Color.red);
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
	}
}
