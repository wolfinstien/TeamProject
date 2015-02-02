using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SuperCubeState { 
    Switching,
    Still
}

public enum SwitchPhase { 
    PhaseOne,
    PhaseTwo,
    PhaseThree
}

public enum SpaceRelative { 
    Front,
    Behind,
    Above,
    Below,
    Left,
    Right
}

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

    public T1 Item1 {
        get { return this.m_a; }
    }

    public T2 Item2 {
        get { return this.m_b; }
    }

    public T3 Item3 {
        get { return this.m_c; }
    }

    /// <summary>
    /// Accessor for creating instances of Triple.
    /// </summary>
    /// <param name="item1">
    /// The first item.
    /// </param>
    /// <param name="item2">
    /// The second item.
    /// </param>
    /// <param name="item3">
    /// The third item.
    /// </param>
    /// <returns>
    /// A new instance of Triple.
    /// </returns>
    public static Triple<T1, T2, T3> Create(T1 item1, T2 item2, T3 item3) {
        return new Triple<T1, T2, T3>(item1, item2, item3);
    }

    /*
     * Conversion operators, to aid in quick, simple conversions between
     * Vector3 and a Triple of floats.
     */

    //public static implicit operator Triple<float, float, float>(Vector3 rhs) {
    //    return new Triple<float, float, float>(rhs.x, rhs.y, rhs.z);
    //}

    //public static implicit operator Vector3(Triple<float, float, float> rhs) {
    //    return new Vector3(rhs.Item1, rhs.Item2, rhs.Item3);
    //}

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
                        STEP_Y = 5f,
                        SWITCH_SPEED = 2f;

    // References to the cubling prefabs
    public GameObject Room0, Room1, Room2, Room3, Room4, Room5, Room6, Room7, Room8, Room9,
                     Room10, Room11, Room12, Room13, Room14, Room15, Room16, Room17, Room18,
                     Room19, Room20, Room21, Room22, Room23, Room24, Room25;

    public GameObject m_switcherA; /*, m_switcherB, m_switcherC;*/
    private Vector3 m_centralVector, m_lastPosition, m_targetPosition;
    private Triple<int, int, int> m_lastLocation, m_pALocation;

    // Multidimensional array to hold each cubling
    private GameObject[, ,] m_cublings;

    // Enum Variables
    private SuperCubeState m_scState;
    private SpaceRelative m_relative;
    private SwitchPhase m_phase;

	// Use this for initialization
	void Start () {

        m_scState = SuperCubeState.Still;
        
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

        m_centralVector = GetCublingVector(1, 1, 1);
	}

    // Update is called once per frame
    void Update() {

        /***** DEBUG: PERFORM SWITCH *****/
        if (Input.GetKeyDown(KeyCode.S)) {
            m_scState = SuperCubeState.Switching;
            m_phase = SwitchPhase.PhaseOne;
            Switch();
        }

        if (m_scState.Equals(SuperCubeState.Switching)) {
            //if (m_phase.Equals(SwitchPhase.PhaseOne) && m_switcherA.transform.position.Equals(m_targetPosition)) {
            //    m_phase = SwitchPhase.PhaseTwo;
            //    m_switcherA.transform.position.Set(
            //        (float)System.Math.Round(m_targetPosition.x, 2), 
            //        (float)System.Math.Round(m_targetPosition.y, 2), 
            //        (float)System.Math.Round(m_targetPosition.z, 2));
            //    //m_cublings[1, 1, 1] = m_switcherA;
            //    //m_switcherA = null;
            //    Switch();
            //}

            //if (m_phase.Equals(SwitchPhase.PhaseTwo) && m_switcherA.transform.position.Equals(m_targetPosition)) {
            //    m_phase = SwitchPhase.PhaseThree;
            //    m_switcherA.transform.position.Set(
            //        (float)System.Math.Round(m_targetPosition.x, 2),
            //        (float)System.Math.Round(m_targetPosition.y, 2),
            //        (float)System.Math.Round(m_targetPosition.z, 2));
            //    //m_cublings[m_pALocation.Item1, m_pALocation.Item2, m_pALocation.Item3] = m_switcherA;
            //    //m_switcherA = null;
            //    // Switch();
            //}
            
            switch (m_relative) { 
                case SpaceRelative.Above:
                    if (m_switcherA.transform.position.y > m_targetPosition.y)
                        m_switcherA.transform.Translate(Vector3.down * SWITCH_SPEED * Time.deltaTime);
                    else {
                        Next();
                    }
                    break;
                case SpaceRelative.Below:
                    if (m_switcherA.transform.position.y < m_targetPosition.y)
                        m_switcherA.transform.Translate(Vector3.up * SWITCH_SPEED * Time.deltaTime);
                    else {
                        Next();
                    }
                    break;
                case SpaceRelative.Left:
                    if (m_switcherA.transform.position.x < m_targetPosition.x)
                        m_switcherA.transform.Translate(Vector3.right * SWITCH_SPEED * Time.deltaTime);
                    else {
                        Next();
                    }
                    break;
                case SpaceRelative.Right:
                    if (m_switcherA.transform.position.x > m_targetPosition.x)
                        m_switcherA.transform.Translate(Vector3.left * SWITCH_SPEED * Time.deltaTime);
                    else {
                        Next();
                    }
                    break;
                case SpaceRelative.Front:
                    if (m_switcherA.transform.position.z > m_targetPosition.z)
                        m_switcherA.transform.Translate(Vector3.back * SWITCH_SPEED * Time.deltaTime);
                    else {
                        Next();
                    }
                    break;
                case SpaceRelative.Behind:
                    if (m_switcherA.transform.position.z < m_targetPosition.z)
                        m_switcherA.transform.Translate(Vector3.forward * SWITCH_SPEED * Time.deltaTime);
                    else {
                        Next();
                    }
                    break;
                default:
                    break;
            }
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
        return new Vector3(x * STEP_X, y * STEP_Y, z * STEP_X); 
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

    private void Next() {
        m_switcherA.transform.position.Set(
                            (float)System.Math.Round(m_targetPosition.x, 2),
                            (float)System.Math.Round(m_targetPosition.y, 2),
                            (float)System.Math.Round(m_targetPosition.z, 2));
        if (m_phase.Equals(SwitchPhase.PhaseOne)) {
            m_phase = SwitchPhase.PhaseTwo;
            Switch();
        }
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
        

        // Now lets choose a cubling from the list at random
        System.Random r = new System.Random();

        if (m_phase.Equals(SwitchPhase.PhaseOne)) {
            List<GameObject> centrals = new List<GameObject>() { 
                m_cublings[1, 0, 1],
                m_cublings[1, 1, 0],
                m_cublings[0, 1, 1],
                m_cublings[2, 1, 1],
                m_cublings[1, 1, 2],
                m_cublings[1, 2, 1]
            };
            
            m_switcherA = centrals[r.Next(0, centrals.Count)];
            m_pALocation = GetCublingLocation(m_switcherA.transform.position);
            m_targetPosition = m_centralVector;
        }

        if (m_phase.Equals(SwitchPhase.PhaseTwo)) {
            List<GameObject> p2 = new List<GameObject>();
            
            switch (m_relative) { 
                case SpaceRelative.Above:
                    p2.Add(m_cublings[1, 2, 0]);
                    p2.Add(m_cublings[0, 2, 1]);
                    p2.Add(m_cublings[2, 2, 1]);
                    p2.Add(m_cublings[1, 2, 2]);
                    break;
                case SpaceRelative.Below:
                    p2.Add(m_cublings[1, 0, 0]);
                    p2.Add(m_cublings[0, 0, 1]);
                    p2.Add(m_cublings[2, 0, 1]);
                    p2.Add(m_cublings[1, 0, 2]);
                    break;
                case SpaceRelative.Left:
                    p2.Add(m_cublings[0, 0, 1]);
                    p2.Add(m_cublings[0, 1, 0]);
                    p2.Add(m_cublings[0, 1, 2]);
                    p2.Add(m_cublings[0, 2, 1]);
                    break;
                case SpaceRelative.Right:
                    p2.Add(m_cublings[2, 0, 1]);
                    p2.Add(m_cublings[2, 1, 0]);
                    p2.Add(m_cublings[2, 1, 2]);
                    p2.Add(m_cublings[2, 2, 1]);
                    break;
                case SpaceRelative.Behind:
                    p2.Add(m_cublings[1, 0, 0]);
                    p2.Add(m_cublings[0, 1, 0]);
                    p2.Add(m_cublings[2, 1, 0]);
                    p2.Add(m_cublings[1, 2, 0]);
                    break;
                case SpaceRelative.Front:
                    p2.Add(m_cublings[1, 0, 2]);
                    p2.Add(m_cublings[0, 1, 2]);
                    p2.Add(m_cublings[2, 1, 2]);
                    p2.Add(m_cublings[1, 2, 2]);
                    break;
                default:
                    break;
            }

            m_switcherA = p2[r.Next(0, p2.Count)];
            m_targetPosition = m_lastPosition;
        }

        m_lastPosition = m_switcherA.transform.position;
        m_lastLocation = GetCublingLocation(m_lastPosition);

        if (m_lastLocation.Item1 < 1) m_relative = SpaceRelative.Left;
        else if (m_lastLocation.Item1 > 1) m_relative = SpaceRelative.Right;
        if (m_lastLocation.Item2 < 1) m_relative = SpaceRelative.Below;
        else if (m_lastLocation.Item2 > 1) m_relative = SpaceRelative.Above;
        if (m_lastLocation.Item3 < 1) m_relative = SpaceRelative.Behind;
        else if (m_lastLocation.Item3 > 1) m_relative = SpaceRelative.Front;
    }
}
