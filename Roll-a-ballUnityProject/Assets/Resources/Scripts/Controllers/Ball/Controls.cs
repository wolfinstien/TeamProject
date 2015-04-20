using UnityEngine;
using System.Collections;

#region Enumerators

public enum Relativity
{ 
    Ground = 1,
    Wall = Ground << 1,
    Ceiling = Ground << 2,
    Sinking = Ground << 3
}

#endregion

public class Controls : MonoBehaviour 
{
    #region Members

    public float currentSpeed;
    public float maxSpeed;
	private float distToGround;
    private AudioSource m_impactSfx, m_pickUpSfx;
    public AudioClip[] collisionSounds;

    #endregion

    #region Properties

    public Relativity BallRelativity { get; private set; }
    public Vector3 WallDirectional { get; private set; }
    public string PreviousRoom { get; private set; }
    public string CurrentRoom { get; private set; }

    #endregion

    #region Functions

    /*
     * Listeners
     */

    public void Set_BallRelativity(Relativity r) 
    {
        BallRelativity = r;
    }

    public void Set_WallDirection(Vector3 v) 
    {
        WallDirectional = v;
    }

    public void Set_PreviousRoom(string pr) 
    {
        PreviousRoom = pr;
    }

    public void Set_CurrentRoom(string cr) 
    {
        CurrentRoom = cr;
    }

    public void Set_BallColour(Color color) 
    {
        this.GetComponent<Renderer>().material.color = color;
    }

    public void Rescale() 
    {
        this.transform.localScale = Vector3.one;
        GetComponent<ParticleSystem>().enableEmission = false;
    }

    /*
     * Initilaizers
     */

	// Use this for initialization
	void Start () 
	{
        //Physics.gravity.Set(0.0f,-1000f,0.0f);
        maxSpeed = 10f;
        BallRelativity = Relativity.Ground;
        WallDirectional = Vector3.zero;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		distToGround = GetComponent<Collider>().bounds.extents.y;
        GetComponent<ParticleSystem>().enableEmission = false;
        
        // Set up audio sources
        AudioSource[] _as = GetComponents<AudioSource>();
        m_impactSfx = _as[0];
        m_pickUpSfx = _as[1];

        /* TODO: 
         * We need to implement a means of determining which room we're in, 
         * rather than this hardcode style.
         * 
         * NOTE:
         * As long as we are using this, you must also place the ball in the
         * same room as defined here, for the spawn initial detection.
         * If you don't, it'll all go pear-shaped once you fall into a trap.
         * * I say once, because you will (you'd better; otherwise this
         * game is too easy).
         */
        PreviousRoom = CurrentRoom = "Room_4";
        this.transform.position = GetComponent<SpawnPoint>().GetLocationFrom(CurrentRoom);
	}

    /*
     * Logic
     */

	// Update is called once per frame
	void Update () 
	{
		currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;

        //if (!IsGrounded()) Debug.Log("Airbourne");
        //else Debug.Log("Grounded");

        //Debug.Log(this.transform.position.y.ToString());

        if (!BallRelativity.Equals(Relativity.Sinking)) {

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            /* 
         * Check for player keyboard input and move ball accordingly
         */
            maxSpeed = 12.5f;
            if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed) 
            {
                if (isGrounded() || BallRelativity.Equals(Relativity.Wall)) 
                {
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
                    {
                        GetComponent<Rigidbody>().AddForce(Vector3.forward * maxSpeed);
                    }
                    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
                    {
                        GetComponent<Rigidbody>().AddForce(Vector3.back * maxSpeed);
                    }
                    if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)) 
                    {
                        if (BallRelativity.Equals(Relativity.Wall))
                        {
                            GetComponent<Rigidbody>().AddForce(Vector3.up * (maxSpeed * 0.5f));
                        }
                        else
                        {
                            GetComponent<Rigidbody>().AddForce(Vector3.left * maxSpeed);
                        }
                    }
                    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
                    {
                        GetComponent<Rigidbody>().AddForce(Vector3.right * maxSpeed);
                    }
                }
            }
#endif

#if UNITY_ANDROID
            /*
             * Move ball based on accelerometer values
             */
            Vector3 movement = new Vector3(Input.acceleration.x, 0f, Input.acceleration.y); //-y

            if (movement.sqrMagnitude > 1)
            {
                movement.Normalize();
            }
        
		    if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed && isGrounded())
		    {
        	    GetComponent<Rigidbody>().AddForce(movement * 50f);
		    }
#endif
        }
	}

    void FixedUpdate() 
    {
        switch (BallRelativity) 
        { 
            case Relativity.Ground:
                // Simulates normal gravity
                GetComponent<Rigidbody>().AddForce(-Vector3.up * Physics.gravity.magnitude);
                break;
            case Relativity.Wall:
                GetComponent<Rigidbody>().AddForce(WallDirectional * Physics.gravity.magnitude);
                break;
            case Relativity.Ceiling:
                GetComponent<Rigidbody>().AddForce(Vector3.up * Physics.gravity.magnitude);
                break;
            case Relativity.Sinking:
                GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
                GetComponent<Rigidbody>().AddForce(-Vector3.up * 0.05f, ForceMode.VelocityChange);
                this.transform.localScale -= new Vector3(0.25f, 0.25f, 0.25f) * Time.deltaTime;
                break;
        }
    }

    public bool isGrounded() 
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    /*
     * Triggers/Collisions
     */

	//added by adam
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickup")
		{
			this.GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;
			other.gameObject.SetActive(false);
            m_pickUpSfx.Play();
		}
	}

    void OnCollisionEnter(Collision other)
    {
        Ray colRay = new Ray();
        colRay.origin = transform.position;
        colRay.direction = other.contacts[0].point - this.transform.position;

        // dont want to colide with objects we are rollling on
        if (colRay.direction.y > -.1)
        {
            this.GetComponent<AudioSource>().PlayOneShot(collisionSounds[Random.Range(0, collisionSounds.Length)]);
#if UNITY_ANDROID
			Handheld.Vibrate ();
#endif
        }
    }
    #endregion
}

// END OF FILE