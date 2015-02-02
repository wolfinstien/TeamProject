using UnityEngine;
using System.Collections;

public class PushBlock : MonoBehaviour {

    #region Fields

    private const float m_boundsNode_A = 5f, 
                        m_boundsNode_B = -2f;

    #endregion

    #region Properties

    public int MaxDistance { get; set; }

    #endregion

    #region Functions

    // Use this for initialization
	void Start () {
        this.rigidbody.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
        GameObject ballRef = GameObject.FindGameObjectWithTag("Player");
        
        if ((this.transform.position.z >= m_boundsNode_A && ballRef.transform.position.z < m_boundsNode_A) ||
            (this.transform.position.z <= m_boundsNode_B && ballRef.transform.position.z > m_boundsNode_B)) {
            this.rigidbody.isKinematic = true;
        }
    }

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag.Equals("Player")) 
            this.rigidbody.isKinematic = false;
		
	}

    void OnCollisionStay(Collision other) {
        if (other.gameObject.tag.Equals("Player")) 
            this.rigidbody.AddForce(0f, 0f, Mathf.Pow(other.gameObject.rigidbody.velocity.z, 2f));
        
    }

    void OnCollisionExit(Collision other) {
        this.rigidbody.isKinematic = true;
    }

    #endregion
}