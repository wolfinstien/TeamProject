#region Prerequisites

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

#endregion

#region Enumerators

public enum MoveAxis 
{ 
    X_,
    Y_,
    Z_
}

#endregion

#region Objects

public class PushBlock : MonoBehaviour 
{

    #region Private Members

    private bool b_ballCollideBounds;

    #endregion

    #region Public Members

    public MoveAxis moveAxis;
    public float ReletiveFrontBound;
    public float ReletiveBackBound;
    float FrontBound;
    float BackBound;

    #endregion

    #region Functions

	void Start () 
    {
        switch (moveAxis)
        {
            case MoveAxis.X_:
                FrontBound = this.transform.position.x + ReletiveFrontBound;
                BackBound = this.transform.position.x + ReletiveBackBound;
                break;
            case MoveAxis.Y_:
                FrontBound = this.transform.position.y + ReletiveFrontBound;
                BackBound = this.transform.position.y + ReletiveBackBound;
                break;
            case MoveAxis.Z_:
                FrontBound = this.transform.position.z + ReletiveFrontBound;
                BackBound = this.transform.position.z + ReletiveBackBound;
                break;
        }
        b_ballCollideBounds = false;
	}

    private string GetRoom() 
    {
        return this.transform.root.name; 
    }

    void Update() { ; }

    void FixedUpdate() 
    {
        switch (moveAxis) 
        { 
            case MoveAxis.X_:
                if ((this.transform.position.x <= this.FrontBound && this.transform.position.x >= this.BackBound) &&
                    !b_ballCollideBounds) 
                {
                    this.GetComponent<Rigidbody>().isKinematic = false;
                }
                else 
                {
                    this.GetComponent<Rigidbody>().isKinematic = true;
                    this.transform.position = new Vector3(Mathf.Round(this.transform.position.x * 2) / 2, this.transform.position.y, this.transform.position.z);
                    //Debug.Log("should have rounded x");
                }
                break;
            case MoveAxis.Y_:
                if ((this.transform.position.y <= this.FrontBound && this.transform.position.y >= this.BackBound) &&
                    !b_ballCollideBounds) 
                {
                    this.GetComponent<Rigidbody>().isKinematic = false;
                }
                else 
                {
                    this.GetComponent<Rigidbody>().isKinematic = true;
                    this.transform.position = new Vector3(this.transform.position.x, Mathf.Round(this.transform.position.y * 2) / 2, this.transform.position.z);
                    //Debug.Log("should have rounded y");
                }
                break;
            case MoveAxis.Z_:
                if ((this.transform.position.z <= this.FrontBound && this.transform.position.z >= this.BackBound) &&
                    !b_ballCollideBounds) 
                {
                    this.GetComponent<Rigidbody>().isKinematic = false;
                }
                else 
                {
                    this.GetComponent<Rigidbody>().isKinematic = true;
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, Mathf.Round(this.transform.position.z * 2) / 2);
                    //Debug.Log("should have rounded z");
                }
                break;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag.Equals("Player")) 
        {
            switch (moveAxis)
            {
                case MoveAxis.X_:
                    if (other.gameObject.transform.position.x > this.BackBound ||
                        other.gameObject.transform.position.x < this.FrontBound) 
                    {
                        b_ballCollideBounds = true;
                    }
                    break;
                case MoveAxis.Y_:
                    if (other.gameObject.transform.position.y > this.FrontBound ||
                        other.gameObject.transform.position.y < this.BackBound)
                    {
                        b_ballCollideBounds = true;
                    }
                    break;
                case MoveAxis.Z_:
                    if (other.gameObject.transform.position.z > this.FrontBound ||
                        other.gameObject.transform.position.z < this.BackBound) 
                    {
                        b_ballCollideBounds = true;
                    }
                    break;
            }
        } 
        else 
        {
            b_ballCollideBounds = false;
        }
    }

    void OnCollisionStay(Collision other)
    {
        b_ballCollideBounds = (other.gameObject.tag.Equals("Player") && 
            other.gameObject.transform.position.y > 0.6f);
    }

    #endregion
}

#endregion

// END OF FILE