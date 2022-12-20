using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameObject mainBall;

    // Update is called once per frame
    void Update()
    {
        rotation();
    }
    void rotation()
    {
        this.transform.eulerAngles = new Vector3 (90,0,0);
    }
}
