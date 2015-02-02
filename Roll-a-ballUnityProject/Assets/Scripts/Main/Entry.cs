using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Quick and dirty class that holds a triple of any type.
 */
public class Triple<T1, T2, T3> {

    private T1 m_a;
    private T2 m_b;
    private T3 m_c;

    protected Triple(T1 a, T2 b, T3 c) {
        this.m_a = a;
        this.m_b = b;
        this.m_c = c;
    }

    public static Triple<T1, T2, T3> Create(T1 item1, T2 item2, T3 item3) {
        return new Triple<T1, T2, T3>(item1, item2, item3);
    }

    public T1 Item1 {
        get { return this.m_a; }
    }

    public T2 Item2 {
        get { return this.m_b; }
    }

    public T3 Item3 {
        get { return this.m_c; }
    }

    public override string ToString() {
        return string.Concat(
            '{', this.Item1.ToString(), ", ", this.Item2.ToString(), ", ", this.Item3.ToString(), '}'); 
    }
}

/*
 * Scene starts from this class.
 */
public class Entry : MonoBehaviour {

    // Positioning Parameters
    private const float STEP_X = 20f,
                        STEP_Y = 5f;

    // References to the cubling prefabs
    public GameObject Room0, Room1, Room2, Room3, Room4, Room5, Room6, Room7, Room8, Room9,
                     Room10, Room11, Room12, Room13, Room14, Room15, Room16, Room17, Room18,
                     Room19, Room20, Room21, Room22, Room23, Room24, Room25;

    // Multidimensional array to hold each cubling
    private GameObject[, ,] m_cublings;

	// Use this for initialization
	void Start () {
        /* 
         * Instantiate & fill array
         */
        m_cublings = new GameObject[3, 3, 3];

        // Bottom Layer
        m_cublings[0, 0, 0] = Room0;      // Start Room
        m_cublings[1, 0, 0] = Room1;
        m_cublings[2, 0, 0] = Room2;
        m_cublings[0, 0, 1] = Room3;
        m_cublings[1, 0, 1] = Room4;
        m_cublings[2, 0, 1] = Room5;
        m_cublings[0, 0, 2] = Room6;
        m_cublings[1, 0, 2] = Room7;
        m_cublings[2, 0, 2] = Room8;

        // Middle Layer
        m_cublings[0, 1, 0] = Room9;
        m_cublings[1, 1, 0] = Room10;
        m_cublings[2, 1, 0] = Room11;
        m_cublings[0, 1, 1] = Room12;
        m_cublings[1, 1, 1] = null;       // Centre Room: left empty for switching
        m_cublings[2, 1, 1] = Room13;
        m_cublings[0, 1, 2] = Room14;
        m_cublings[1, 1, 2] = Room15;
        m_cublings[2, 1, 2] = Room16;

        // Top Layer
        m_cublings[0, 2, 0] = Room17;
        m_cublings[1, 2, 0] = Room18;
        m_cublings[2, 2, 0] = Room19;
        m_cublings[0, 2, 1] = Room20;
        m_cublings[1, 2, 1] = Room21;
        m_cublings[2, 2, 1] = Room22;
        m_cublings[0, 2, 2] = Room23;
        m_cublings[1, 2, 2] = Room24;
        m_cublings[2, 2, 2] = Room25;     // End Room

        // Instantiate all the cubling prefabs
        for (int y = 0; y < 3; ++y) {
            for (int z = 0; z < 3; ++z) {
                for (int x = 0; x < 3; ++x) {
                    if (m_cublings[x, y, z] != null) {
                        m_cublings[x, y, z] = 
                            (GameObject)Instantiate(
                                m_cublings[x, y, z],
                                new Vector3(x * STEP_X, y * STEP_Y, z * STEP_X),
                                Quaternion.identity);
                    }
                }
            }
        }
	}

    // Update is called once per frame
    void Update() {
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    // This moves the top, middle prefab up on every hit on the spacebar
        //    m_cublings[1, 2, 1].transform.Translate(Vector3.up);
        //}

        /***** DEBUG: PERFORM SWITCH *****/
        if (Input.GetKeyDown(KeyCode.S)) {
            Switch();
        }
    }

    /// <summary>
    /// Gets a Vector based on the row, column and aisle specified.
    /// </summary>
    /// <param name="x">
    /// Represents the horizontal cube location.
    /// </param>
    /// <param name="y">
    /// Represents the vertical cube location.
    /// </param>
    /// <param name="z">
    /// Represents the depth of the cube location.
    /// </param>
    /// <returns>
    /// An instance of Vector3.
    /// </returns>
    private Vector3 GetCublingVector(int x, int y, int z) {
        return new Vector3(x * STEP_X, y * STEP_Y, z * STEP_X); // { 20, 5, 20 } -> [ 1, 1, 1 ]
    }

    /// <summary>
    /// Gets the array location of the cube based on its positional vector.
    /// </summary>
    /// <param name="position">
    /// A vector representing the 3-axis position.
    /// </param>
    /// <returns>
    /// A tuple of integers.
    /// </returns>
    private Triple<int, int, int> GetCublingLocation(Vector3 position) {
        return Triple<int, int, int>.Create(
            (int)(position.x % (STEP_X - 1f)), (int)(position.y % (STEP_Y - 1f)), (int)(position.z % (STEP_X - 1f)));
    }

    /// <summary>
    /// Switches the Cublings around.
    /// </summary>
    private void Switch() { 
        /*
         * As the centre of the supercube is the only section that is
         * without a cube, we shall use the empty space to aid switches.
         * 
         * Firstly we need to determine which one of the centre's surrounding
         * cublings (six of) will inherit it's location temporarily.
         * For simplicity, we store the six central cublings in a list.
         */
        List<GameObject> centrals = new List<GameObject>() { 
            m_cublings[1, 0, 1],
            m_cublings[1, 1, 0],
            m_cublings[0, 1, 1],
            m_cublings[2, 1, 1],
            m_cublings[1, 1, 2],
            m_cublings[1, 2, 1]
        };

        // Now lets choose a cubling from the list at random
        System.Random r = new System.Random();
        GameObject toCentre = centrals[r.Next(0, centrals.Count)];
        Triple<int, int, int> newEmpty = GetCublingLocation(toCentre.transform.position);
        Vector3 centralVector = GetCublingVector(1, 1, 1);
    }
}
