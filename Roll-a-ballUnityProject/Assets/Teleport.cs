using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public int level;
    public bool shrink = false;
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        shrink = true;
        player = other.gameObject;
    }

    void Warp()
    {
        if (level!=-1)
        {
            Application.LoadLevel(level);
        }
        else
        {
            // move player to room above
        }
    }

    void Update()
    {
        if (player != null)
        {
            if (shrink && (player.transform.localScale.x > 0.1))
            {
                //float newScale = Mathf.Lerp(1.0f, 0.1f, Time.deltaTime);
                player.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            }

            if (player.transform.localScale.x > 0.1)
            {
                Warp();
            }
        }
    }
}