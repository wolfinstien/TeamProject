using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public string level;
    public bool shrink = false;
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        shrink = true;
        player = other.gameObject;
    }

    void Warp()
    {

    }

    void Update()
    {
        if(shrink && (player.transform.localScale.x>0.1))
        {
            float newScale = Mathf.Lerp(0.1f, 1.0f, Time.deltaTime/1000);
            player.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}