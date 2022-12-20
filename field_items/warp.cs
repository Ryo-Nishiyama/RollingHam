using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warp : MonoBehaviour
{
    float warpAngle = 0;
    float warpCount = 0;
    Color32 warpColor;
    Material Respawn;
    // Start is called before the first frame update
    void Start()
    {
        Respawn = GetComponent<Renderer>().material;
        warpColor = this.gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //cutoff‚Ì’l‚ð3•b‚Å‚P‚É‚·‚é
        Respawn.SetFloat("_cutoff", warpCount);
        this.gameObject.GetComponent<Renderer>().material.color = new Color32(warpColor.r, warpColor.g, warpColor.b, (byte)Mathf.Floor(warpCount*255));
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "player")
        {
            warpCount += Time.deltaTime/3;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "player")
        {
            warpCount = 0;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "player")
        {
            warpCount = 0;
        }
    }
}
