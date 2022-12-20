using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceLance_field : MonoBehaviour
{
    GameObject IceShard;
    GameObject shield;
    AudioClip a_hit;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        IceShard = (GameObject)Resources.Load("attack\\IceShard_field");
        shield = (GameObject)Resources.Load("particle\\shield_Eff");
        a_hit = (AudioClip)Resources.Load("SE\\ice_break2");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.right * ((30 - rb.velocity.magnitude) * 30.0f), ForceMode.Force);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "shield")
        {
            Vector3 hitPoint = collision.ClosestPointOnBounds(this.transform.position);
            Vector3 hitEuler = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y + 90, this.transform.eulerAngles.z);
            Instantiate(shield, hitPoint, Quaternion.Euler(hitEuler));
        }
        Instantiate(IceShard, this.transform.position, Quaternion.Euler(this.transform.eulerAngles));
        AudioSource.PlayClipAtPoint(a_hit, this.transform.position);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(IceShard, this.transform.position, Quaternion.Euler(this.transform.eulerAngles));
        AudioSource.PlayClipAtPoint(a_hit, this.transform.position);
        Destroy(this.gameObject);
    }
}
