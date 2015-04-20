using UnityEngine;
using System.Collections;

public class EnterRoom : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Controls>().currentestRoom = this.gameObject;
        }
    }
}