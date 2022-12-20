using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_sound : MonoBehaviour
{
    AudioSource _AudioSource;
    AudioClip a_hit;
    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        a_hit = (AudioClip)Resources.Load("SE\\hit");
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "enhannceRock":
                AudioSource.PlayClipAtPoint(a_hit, this.transform.position);
                break;
        }
    }
}
